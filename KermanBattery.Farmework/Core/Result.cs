using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Domain.Core
{
    public class Result<T>
    {
        public T Value { get; set; }
        public string ResultMessage { get; set; }
        public int ResultCode { get; set; }

        public static Result<T> Success(ResultInfo resultInfo, T value)
            => new Result<T>
            {
                Value = value,
                ResultMessage = resultInfo.Message,
                ResultCode = resultInfo.Code
            };

        public static Result<T> Success(ResultInfo resultInfo)
            => new Result<T>
            {
                ResultMessage = resultInfo.Message,
                ResultCode = resultInfo.Code
            };
        public static Result<T> Failure(ResultInfo resultInfo)
            => new Result<T>
            {
                ResultMessage = resultInfo.Message,
                ResultCode = resultInfo.Code
            };
    }
    public class ResultForNullValues
    {
        public string ResultMessage { get; set; }
        public int ResultCode { get; set; }

        public static ResultForNullValues Create<T>(Result<T> result)
            => new ResultForNullValues
            { ResultMessage = result.ResultMessage, ResultCode = result.ResultCode };
    }
    public class AppException
    {
        public AppException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
    public class ResultInfo
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ResultInfo(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
