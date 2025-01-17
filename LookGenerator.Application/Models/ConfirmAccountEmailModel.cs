namespace Application.Models ;

    public class ConfirmAccountEmailModel
    {
        public string UserName { get; set; } = string.Empty;
        public string ConfirmationLink { get; set; } = string.Empty;
    }