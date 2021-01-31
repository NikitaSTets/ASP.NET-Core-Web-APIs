using AutoWrapper;

namespace ASP.NET_Core_Web_APIs.Errors
{
    public class MapResponseObject
    {
        [AutoWrapperPropertyMap(Prop.ResponseException)]
        public object Error { get; set; }
    }
}