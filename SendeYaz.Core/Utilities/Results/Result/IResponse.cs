using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Utilities.Results.Result
{
    public interface IResponse
    {
        bool Success { get; }
        string Message { get; }
    }
}
