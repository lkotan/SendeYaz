using System;

namespace SendeYaz.Core.Exceptions
{
    public class SecurityException : Exception
    {
        public SecurityException(string message) : base(message)
        { }
    }
}
