using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.ExternalServices.Model;

namespace OrganikHaberlesme.Mvc.ExternalServices.Base
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
