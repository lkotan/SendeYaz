﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Core.Utilities.Results.Result
{
    public class SuccessResponse:Response
    {
        public SuccessResponse(string message):base(true,message)
        {

        }
        public SuccessResponse():base(true)
        {

        }
    }
}
