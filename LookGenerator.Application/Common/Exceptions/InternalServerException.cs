namespace Application.Common.Exceptions ;

    public class InternalServerException:Exception
    {
        public InternalServerException()
            : base("An internal server error occurred.") { }

        public InternalServerException(string message)
            : base(message) { }
    }