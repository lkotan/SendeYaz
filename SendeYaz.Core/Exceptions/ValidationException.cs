using System;

namespace SendeYaz.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        { }
    }
}
