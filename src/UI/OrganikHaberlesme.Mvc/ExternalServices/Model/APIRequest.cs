using System.Security.AccessControl;

using MediatR;

namespace OrganikHaberlesme.Mvc.ExternalServices.Model
{
    public class APIRequest:IRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
