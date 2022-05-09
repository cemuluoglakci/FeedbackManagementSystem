using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse(Meta meta)
        {
            this.data = default(T);
            this.Meta = meta;
        }
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

        public Meta Meta { get; set; }
        public T? data { get; set; }

        public static BaseResponse<T> Fail(string failureMessage)
        {
            Meta meta = new Meta() 
            { 
                Message = failureMessage,
                SuccessStatus = false
            };
            return new BaseResponse<T>(meta);
        }
    }

    public class Meta
    {
        public bool SuccessStatus { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
