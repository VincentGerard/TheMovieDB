namespace WebServerApi.Controllers.V1
{
    public class ResponseCreated<T> : ResponseSingleObject<T>
    {
        public string LocationUri { get; set; }
    }
}