using System;

namespace SendeYaz.Core.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        { }
    }
}
