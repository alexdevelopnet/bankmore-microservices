using BankMore.Auth.Application.Commands;
using BankMore.Auth.Domain.Abstractions;
using BankMore.Auth.Domain.Repositories;
using BankMore.Auth.Infrastructure.Persistence;
using BankMore.Auth.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependência
builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var factory = sp.GetRequiredService<IDbConnectionFactory>();
    return factory.CreateConnection();
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CriarUsuarioCommand).Assembly));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de exceções
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
