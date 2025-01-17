namespace Application.Exceptions ;

    public class BadRequestException: Exception
    {
        public BadRequestException()
            : base("The request was invalid or cannot be otherwise served.") { }

        public BadRequestException(string message)
            : base(message) { }
    }