using Microsoft.Build.Utilities;

using OrganikHaberlesme.Application.Contracts.Infrastructure;
using OrganikHaberlesme.Application.Models.Email;

using OrganikHaberlesme.Infrastructure.Mail;
using OrganikHaberlesme.Mvc.ExternalServices.Model.OrganikAPI;
using OrganikHaberlesme.Mvc.ExternalServices.Services.IServices;
using OrganikHaberlesme.Mvc.Services.Base;

namespace OrganikHaberlesme.Mvc.BackgroundJobs
{
    public static class FireAndForget
    {
        public static void EmailSendToUser(VerificationNotify notify)
        {
            var result = Hangfire.BackgroundJob.Enqueue<IEmailSender>(
                x => x.SendEmail(
                  new Email
                  {
                      Body = notify.Message,
                      To = notify.MailTo,
                      Subject = $"Email Doğrulama Kodunuz: {notify.Code}"
                  }));
        }

        public static void SMSSendToUser(VerificationNotify notify)
        {
            var result = Hangfire.BackgroundJob.Enqueue<ISmsSender>(
                x => x.SendSmsAsync(notify));
        }
    }
}
