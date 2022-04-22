using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse(T data)
        {
            this.data = data;
            this.Meta = new Meta();
        }
        public BaseResponse(T data, string message)
        {
            this.data = data;
            this.Meta = new Meta 
            {
                Message = message,
                SuccessStatus = false
            };
        }
        public BaseResponse(string message)
        {
            this.data = default;
            this.Meta = new Meta
            {
                Message = message,
                SuccessStatus = false
            };
        }
        public Meta Meta { get; set; }
        public T? data { get; set; }

        public static BaseResponse<T> Fail(string failureMessage)
        {
            return new BaseResponse<T>(failureMessage);
        }
    }

    public class Meta
    {
        public bool SuccessStatus { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
