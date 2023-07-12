using OrganikHaberlesme.Mvc.ExternalServices.Base;
using OrganikHaberlesme.Mvc.ExternalServices.Model;
using System.Net.Http;
using System.Threading.Tasks;
using OrganikHaberlesme.Mvc.ExternalServices.Services.IServices;
using Microsoft.Extensions.Configuration;
using OrganikHaberlesme.Mvc.ExternalServices.Model.OrganikAPI;
using Humanizer;

namespace OrganikHaberlesme.Mvc.ExternalServices.Services
{

    public class OrganikSmsService : BaseService, IOrganikSmsService
    {
     
        private string OrganikAPIUrl;

        public OrganikSmsService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            OrganikAPIUrl = configuration.GetValue<string>("ServiceUrls:OrganikAPI");
        }
        

        public Task<SendSMSResponse> CreateAsync(SendSMSRequest smsRequest)
        { 
            return SendAsync<SendSMSResponse>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = smsRequest,
                Url = OrganikAPIUrl + "/sms/send"
                //Token = token
            });
        }




    }
}

