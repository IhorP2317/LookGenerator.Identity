namespace Application.Exceptions ;

    public class AccessForbiddenException:Exception
    {
        public AccessForbiddenException()
            : base("Access denied!") { }

        public AccessForbiddenException(string message)
            : base(message) { }
    }