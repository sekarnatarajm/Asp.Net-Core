namespace StudentManagement.API.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, Exception exception) : base(message, exception) { }
        public ValidationException(string message) : base(message) { }
    }
}
