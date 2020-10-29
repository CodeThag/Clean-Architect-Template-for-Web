using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public class ResultModel
    {
        internal ResultModel(bool status, string responseMsg)
        {
            Status = status;
            ResponseMsg = responseMsg;
        }

        internal ResultModel(bool status, string responseMsg, object payload)
        {
            Status = status;
            ResponseMsg = responseMsg;
            Payload = payload;
        }

        public string ResponseMsg { get; set; }
        public bool Status { get; set; }
        public object Payload { get; set; }

        public static ResultModel Success(string responseMsg)
        {
            return new ResultModel(true, responseMsg);
        }

        public static ResultModel Success(string responseMsg, object payload)
        {
            return new ResultModel(true, responseMsg, payload);
        }

        public static ResultModel Failure(string responseMsg)
        {
            return new ResultModel(false, responseMsg);
        }
    }
}
