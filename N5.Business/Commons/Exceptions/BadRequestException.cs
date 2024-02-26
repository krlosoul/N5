namespace N5.Business.Commons.Exceptions
{
	public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}