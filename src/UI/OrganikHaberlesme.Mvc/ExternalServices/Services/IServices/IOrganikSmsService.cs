using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.ExternalServices.Base;
using OrganikHaberlesme.Mvc.ExternalServices.Model.OrganikAPI;

namespace OrganikHaberlesme.Mvc.ExternalServices.Services.IServices
{
    public interface IOrganikSmsService
    {
        //Task<T> GetAllAsync<T>(string token);
        //Task<T> GetAsync<T>(int id, string token);
        Task<SendSMSResponse> CreateAsync(SendSMSRequest smsRequest);
        //Task<T> UpdateAsync<T>(SmsUpdateDTO dto, string token);
        //Task<T> DeleteAsync<T>(int id, string token);

    }
}
