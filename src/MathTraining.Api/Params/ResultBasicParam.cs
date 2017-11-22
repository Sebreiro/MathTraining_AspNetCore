using System;
using System.Collections.Generic;
using System.Text;

namespace MathTraining.Api.Params
{
    public class ResultBasicParam
    {

        public bool Success;
        public string Message;

        public ResultBasicParam(bool success, string message)
        {
            Success = success;
            Message = message;
        }

    }
}
