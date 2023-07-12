using Microsoft.Extensions.Options;

using OrganikHaberlesme.Mvc.ExternalServices.Model.OrganikAPI;
using OrganikHaberlesme.Mvc.ExternalServices.Services.IServices;
using OrganikHaberlesme.Mvc.Services.Base;

using System.Collections.Generic;
using System.Threading.Tasks;

using static AutoMapper.Internal.ExpressionFactory;

namespace OrganikHaberlesme.Mvc.ExternalServices.Services
{
    public class MessageSender : ISmsSender
    {
        private readonly IOrganikSmsService _organikSmsService;
        public MessageSender(IOrganikSmsService organikSmsService)
        {
            _organikSmsService = organikSmsService;
        }

        public async Task<bool> SendSmsAsync(VerificationNotify verification)
        {
            var smsRequest = new SendSMSRequest
            {
                message = $"2FA kontrollü giriş için doğrulama kodunuz: {verification.Code}\n Kimseyle paylaşmayınız!",
                header = "100677",
                type = "sms",
                otp = false,
                date = "Hemen",
                appeal = false,
                validity = 48,
                recipients = new List<string>()
            };
            smsRequest.recipients.Add(verification.PhoneNumber);

            var result= await _organikSmsService.CreateAsync(smsRequest);
            return result.result;
        }
    }
}
