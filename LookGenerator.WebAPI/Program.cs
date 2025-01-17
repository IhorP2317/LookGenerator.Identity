using Application;
using FastEndpoints;
using LookGenerator.Infrastructure;
using LookGenerator.Persistence;
using LookGenerator.WebAPI;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Configuration.AddUserSecrets<Program>();
    builder.Services.ConfigureWebApi(builder.Configuration);
    builder.Services.ConfigureApplication();
    builder.Services.ConfigurePersistence(builder.Configuration);
    builder.Services.ConfigureInfrastructure(builder.Configuration, builder.Environment);
    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }
    app
        .UseExceptionHandler()
        .UseCors("AllowAny")
        .UseAuthentication() 
        .UseAuthorization()
        .UseFastEndpoints();
    app.Run();
    
