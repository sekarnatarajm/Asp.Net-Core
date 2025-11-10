namespace StudentManagement.API.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, Exception exception) : base(message, exception) { }
        public NotFoundException(string message) : base(message) { }
    }
}
