using AutoWrapper;

namespace ASP.NET_Core_Web_APIs
{
    public class MapResponseObject
    {
        [AutoWrapperPropertyMap(Prop.ResponseException)]
        public object Error { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }

        public string Code { get; set; }
        public InnerError InnerError { get; set; }

        public Error(string message, string code, InnerError inner)
        {
            Message = message;
            Code = code;
            InnerError = inner;
        }

    }

    public class InnerError
    {
        public string RequestId { get; set; }
        public string Date { get; set; }

        public InnerError(string reqId, string reqDate)
        {
            RequestId = reqId;
            Date = reqDate;
        }
    }
}