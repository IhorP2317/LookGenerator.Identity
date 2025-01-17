using Application.Abstractions;
using Application.Exceptions;
using Application.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Hosting;

namespace LookGenerator.Infrastructure.Services ;

    public class EmailService(IFluentEmail fluentEmail, IWebHostEnvironment environment ) : IEmailService
    {
        private readonly string _templateDirectory = Path.Combine(environment.ContentRootPath, "EmailTemplates");
        public async Task SendEmailAsync(SendEmailOptions options, CancellationToken cancellationToken = default)
        {
           
            if (string.IsNullOrEmpty(options.To))
                throw new BadRequestException("Recipient 'To' address is required");
            if (string.IsNullOrEmpty(options.Subject))
                throw new BadRequestException("Email 'Subject' is required");

           
            var fluent = fluentEmail
                .To(options.To)
                .Subject(options.Subject);

           
            if (!string.IsNullOrWhiteSpace(options.TemplateFileName) && options.Model is not null)
            {
                var fullTemplatePath = Path.Combine(_templateDirectory, options.TemplateFileName);

                if (!File.Exists(fullTemplatePath))
                    throw new NotFoundException($"Email template file not found at path: {fullTemplatePath}");

                fluent = fluent.UsingTemplateFromFile(fullTemplatePath, options.Model, isHtml: true);
            }
           
            else if (!string.IsNullOrWhiteSpace(options.Body))
            {
                fluent = fluent.Body(options.Body, isHtml: true);
            }
            else
            {
               
                throw new BadRequestException("Either a template and model, or a raw Body must be provided.");
            }
            
            var response = await fluent.SendAsync(cancellationToken);

            if (!response.Successful)
            {
                var errors = response.ErrorMessages?.Count > 0
                    ? string.Join(", ", response.ErrorMessages)
                    : "No error messages returned";

                throw new InternalServerException($"Failed to send email to {options.To}. Errors: {errors}");
            }
        }
        
    }