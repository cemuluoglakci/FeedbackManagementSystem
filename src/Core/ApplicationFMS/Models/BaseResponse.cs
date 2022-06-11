namespace ApplicationFMS.Models
{
    public class BaseResponse
    {
        public BaseResponse(Meta meta)
        {
            this.data = null;
            this.Meta = meta;
        }
        public BaseResponse(object data)
        {
            this.data = data;
            this.Meta = new Meta();
        }
        public BaseResponse(object data, string message)
        {
            this.data = data;
            this.Meta = new Meta
            {
                Message = message,
                SuccessStatus = false
            };
        }

        public Meta Meta { get; set; }
        public object? data { get; set; }

        public static BaseResponse Fail(string failureMessage)
        {
            Meta meta = new Meta()
            {
                Message = failureMessage,
                SuccessStatus = false
            };
            return new BaseResponse(meta);
        }
    }

    public class Meta
    {
        public bool SuccessStatus { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
