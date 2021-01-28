using AutoWrapper;

namespace ASP.NET_Core_Web_APIs
{
    public class MapResponseObject
    {
        [AutoWrapperPropertyMap(Prop.StatusCode)]
        public string Code { get; set; }

        [AutoWrapperPropertyMap(Prop.ResponseException_ExceptionMessage)]
        public string Message { get; set; }

        [AutoWrapperPropertyMap(Prop.Result)] 
        public object Data { get; set; }

        [AutoWrapperPropertyMap(Prop.ResponseException)]
        public object Error { get; set; }

        [AutoWrapperPropertyMap(Prop.ResponseException_Details)]
        public string StackTrace { get; set; }
    }
}