using System.Net;
using System.Net.Mail;
using Application.Abstractions;
using LookGenerator.Infrastructure.Services;
using LookGenerator.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LookGenerator.Infrastructure ;

    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
            var templatePath = Path.Combine(environment.ContentRootPath, "EmailTemplates");
            var mailSettings = configuration.GetSection(nameof(MailSettings)).Get<MailSettings>(); 
            services.AddFluentEmail(mailSettings!.SenderMail)
                .AddRazorRenderer(templatePath)
                .AddSmtpSender(() =>
                {
                    var smtpClient = new SmtpClient(mailSettings.SmtpHost, mailSettings.SmtpPort)
                    {
                        Credentials = new NetworkCredential(mailSettings.SenderMail, mailSettings.AuthPassword),
                        EnableSsl = true
                    };
                    return smtpClient;
                });
            services.AddScoped<IEmailService, EmailService>();
        }
    }