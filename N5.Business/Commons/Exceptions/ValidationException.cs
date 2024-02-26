namespace N5.Business.Commons.Exceptions
{
    using System;
    using FluentValidation.Results;

    public class ValidationException : Exception
    {
        public ValidationException() : base("Una o mas validaciones han fallado.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}

