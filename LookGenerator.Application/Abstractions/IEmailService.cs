using Application.Models;

namespace Application.Abstractions ;

    public interface IEmailService
    {
        Task SendEmailAsync(SendEmailOptions options, CancellationToken cancellationToken = default);
    }