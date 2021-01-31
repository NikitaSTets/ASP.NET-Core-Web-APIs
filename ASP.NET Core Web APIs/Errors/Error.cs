namespace ASP.NET_Core_Web_APIs.Errors
{
    public class Error
    {
        public ErrorCodes Code { get; set; }

        public string Message { get; set; }

        public InnerError InnerError { get; set; }

        public string Target { get; set; }


        public Error(string message, ErrorCodes code, string target = default, InnerError inner = default)
        {
            Message = message;
            Code = code;
            Target = target;
            InnerError = inner;
        }
    }
}