using LookGenerator.Domain.Constants;

namespace Application.Exceptions ;

    public class ValidationException:Exception
    {
        public List<Error> Errors { get; }

        public ValidationException()
            : base("Validation Failed")
        {
            Errors = new List<Error>();
        }

        public ValidationException(IEnumerable<Error> errors)
            : base("Validation Failed")
        {
            Errors = errors.ToList();
        }

        public ValidationException(string message, IEnumerable<Error> errors)
            : base(message)
        {
            Errors = errors.ToList();
        }
    }