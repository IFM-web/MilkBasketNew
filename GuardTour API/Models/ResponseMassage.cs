using System.Net;

namespace GuardTour_API.Models
{
    public class ResponseMassage
    {
        public string Massage {  get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public object Data { get; set; }
    }
}
