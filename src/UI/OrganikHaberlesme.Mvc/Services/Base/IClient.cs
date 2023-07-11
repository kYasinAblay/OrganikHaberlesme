using System.Net.Http;

namespace OrganikHaberlesme.Mvc.Services.Base
{
    public partial interface IClient
    {
       HttpClient HttpClient { get; }
    }
}

