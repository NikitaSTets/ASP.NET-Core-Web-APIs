namespace ASP.NET_Core_Web_APIs.Errors
{
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