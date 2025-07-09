using BankMore.Auth.Application.Commands;
using BankMore.Auth.Domain.Abstractions;
using BankMore.Auth.Domain.Repositories;
using BankMore.Auth.Infrastructure.Persistence;
using BankMore.Auth.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddJsonFile("appsettings.Docker.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Detectar se está rodando no Docker
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

// Registrar fábrica de conexões
builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

// Registrar IDbConnection
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var factory = sp.GetRequiredService<IDbConnectionFactory>();
    return factory.CreateConnection();
});

// Injeção condicional de repositórios
if (isDocker)
{
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryMySql>();
    builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepositoryMySql>();
    builder.Services.AddScoped<IMovimentoRepository, MovimentoRepositoryMySql>();
    builder.Services.AddScoped<ITransferenciaRepository, TransferenciaRepositoryMySql>();
    builder.Services.AddScoped<IIdempotenciaRepository, IdempotenciaRepositoryMySql>();
}
else
{
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositorySqlServer>();
    builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepositorySqlServer>();
    builder.Services.AddScoped<IMovimentoRepository, MovimentoRepositorySqlServer>();
    builder.Services.AddScoped<ITransferenciaRepository, TransferenciaRepositorySqlServer>();
    builder.Services.AddScoped<IIdempotenciaRepository, IdempotenciaRepositorySqlServer>();
}

// MediatR e HttpContextAccessor para pegar dados do usuário no contexto
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CriarUsuarioCommand).Assembly));
builder.Services.AddHttpContextAccessor();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configuração JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");

// Validação para garantir que a chave secreta não seja nula ou vazia
if (string.IsNullOrEmpty(secretKey))
{
    throw new Exception("JWT SecretKey não está configurada no appsettings.json");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero // importante para evitar delay na expiração do token
        };
    });

var app = builder.Build();

// Middleware Swagger em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware global de erro
app.UseExceptionHandler(exceptionApp =>
{
    exceptionApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var problem = exception is InvalidOperationException
            ? new { Status = 400, Title = "Erro de validação", Detail = exception.Message }
            : new { Status = 500, Title = "Erro interno", Detail = "Ocorreu um erro inesperado." };

        context.Response.StatusCode = problem.Status;
        await context.Response.WriteAsJsonAsync(problem);
    });
});

app.UseHttpsRedirection();

// Autenticação e autorização precisam vir nesta ordem
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
