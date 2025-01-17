namespace Application.Models ;

    public class SendEmailOptions
    {
        public required string To { get; set; } = string.Empty;
        public required string Subject { get; set; } = string.Empty;

        
        public string? Body { get; set; }

       
        public string? TemplateFileName { get; set; }
        public object? Model { get; set; }
    }