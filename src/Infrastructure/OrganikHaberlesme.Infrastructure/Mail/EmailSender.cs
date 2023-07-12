using System.Net;
using System.Threading.Tasks;

using OrganikHaberlesme.Application.Contracts.Infrastructure;
using OrganikHaberlesme.Application.Models.Email;

using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace OrganikHaberlesme.Infrastructure.Mail
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmail(Email email)
        {
            
                var client = new SendGridClient(_emailSettings.ApiKey);
                var to = new EmailAddress(email.To,"Gönderilen Send Grid");
                var from = new EmailAddress
                {
                    Email = _emailSettings.FromAddress,
                    Name = _emailSettings.FromName
                };

            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(message);

                return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}
