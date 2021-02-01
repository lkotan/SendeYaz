﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Utilities.Results.DataResult
{
    public class SuccessDataResponse<T>:DataResponse<T>
    {
        public SuccessDataResponse(T data,string message):base(data,true,message)
        {

        }
        public SuccessDataResponse(T data):base(data,true)
        {

        }
        public SuccessDataResponse(string message) : base(default, true, message)
        {

        }

        public SuccessDataResponse() : base(default, true)
        {

        }
    }
}
