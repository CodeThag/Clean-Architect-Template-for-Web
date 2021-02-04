using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Models
{
    public class Result
    {
        internal Result(bool status)
        {
            Status = status;
        }

        internal Result(bool status, string responseMsg)
        {
            Status = status;
            ResponseMsg = responseMsg;
        }

        internal Result(bool status, string responseMsg, object payload)
        {
            Status = status;
            ResponseMsg = responseMsg;
            Payload = payload;
        }

        public string ResponseMsg { get; set; }
        public bool Status { get; set; }
        public object Payload { get; set; }

        public static Result Success()
        {
            return new Result(true);
        }

        public static Result Success(string responseMsg)
        {
            return new Result(true, responseMsg);
        }
        public static Result Success(object payload)
        {
            return new Result(true, null, payload);
        }

        public static Result Success(string responseMsg, object payload)
        {
            return new Result(true, responseMsg, payload);
        }
        public static Result Failure()
        {
            return new Result(false);
        }

        public static Result Failure(string responseMsg)
        {
            return new Result(false, responseMsg);
        }
        public static Result Failure(string responseMsg, object payload)
        {
            return new Result(false, responseMsg, payload);
        }
    }
}
