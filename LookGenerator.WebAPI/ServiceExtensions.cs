using Application.Abstractions;
using Application.Security.DeleteUser;
using FastEndpoints;
using FastEndpoints.Security;
using LookGenerator.WebAPI.ExceptionHandlers;
using LookGenerator.WebAPI.Services;


namespace LookGenerator.WebAPI ;

    public static class ServiceExtensions
    {
        public static void ConfigureWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddProblemDetails()
                .AddExceptionHandler<CustomExceptionHandler>()
                .AddOpenApi()
                .AddHttpContextAccessor()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddAuthenticationJwtBearer(
                    s =>
                    {
                        s.SigningKey = configuration["AuthSettings:SecretKey"];
                        s.SigningStyle = TokenSigningStyle.Symmetric;
                    },
                    b =>
                    {
                        b.RequireHttpsMetadata = false;
                        b.SaveToken = true;
                        b.TokenValidationParameters.ValidateIssuer = true;
                        b.TokenValidationParameters.ValidIssuer = configuration["AuthSettings:Issuer"];
                        b.TokenValidationParameters.ValidateAudience = true;
                        b.TokenValidationParameters.ValidAudience = configuration["AuthSettings:Audience"];
                        b.TokenValidationParameters.ValidateLifetime = true;
                        b.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                    })
                .AddAuthorization(
                    options =>
                    {
                        options.AddPolicy("CanDeleteUser",
                            policy => { policy.Requirements.Add(new DeleteUserRequirement()); });
                    })
                .AddCors(options =>
                {
                    options.AddPolicy("AllowAny", builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                })
                .AddFastEndpoints();
        }
    }