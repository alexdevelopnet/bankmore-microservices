using BankMore.Auth.Application.Commands;
using BankMore.Auth.Domain.Abstractions;
using BankMore.Auth.Domain.Repositories;
using BankMore.Auth.Infrastructure.Persistence;
using BankMore.Auth.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Carrega configurações em ordem
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddJsonFile("appsettings.Docker.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Detecta ambiente Docker para escolha do DB
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

// Registra o IDbConnectionFactory (implementação pode usar isDocker para decidir)
builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

// Registra o IDbConnection baseado na factory
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var factory = sp.GetRequiredService<IDbConnectionFactory>();
    return factory.CreateConnection();
});

// Registra o repositório correto baseado no ambiente
if (isDocker)
{
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryMySql>();
}
else
{
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositorySqlServer>();
}

// Adiciona MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CriarUsuarioCommand).Assembly));

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// URL amigável lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Middleware Swagger só em Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para tratar exceções com JSON padronizado
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

app.MapControllers();

app.Run();
