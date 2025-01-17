namespace Application.Models ;

    public class SendForgotPasswordEmailModel
    {
        public string UserName { get; set; } = string.Empty;
        public string ResetPasswordLink { get; set; } = string.Empty;
    }