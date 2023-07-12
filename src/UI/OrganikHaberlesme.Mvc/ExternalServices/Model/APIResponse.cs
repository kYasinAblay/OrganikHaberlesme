using System.Collections.Generic;
using System.Net;

using OrganikHaberlesme.Mvc.ExternalServices.Base;

namespace OrganikHaberlesme.Mvc.ExternalServices.Model
{
    public class APIResponse:IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
