namespace StudentManagement.API.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message, Exception exception) : base(message, exception) { }
        public BadRequestException(string message) : base(message) { }
    }
}
