namespace N5.Business.Commons.Exceptions
{
    using System;

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}

