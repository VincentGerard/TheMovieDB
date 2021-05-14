namespace WebServerApi.Controllers.V1
{
    public class ResponsePagine<T> : ResponseSingleObject<T>
    {
        public int From { get; set; }
        public int To { get; set; }
        public string FirstPage { get; set; }
        public string LastPage { get; set; }
        public int TotalRecord { get; set; }
    }
}