using System.Net.Http;

namespace OrganikHaberlesme.Mvc.Services.Base
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient => _httpClient;
    }
}

