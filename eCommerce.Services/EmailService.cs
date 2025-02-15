using eCommerce.Shared.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace eCommerce.Services
{
    public class EmailService : IEmailSender
    {
        private readonly ConfigurationsHelper _configurationsHelper;
        public EmailService(ConfigurationsHelper configurationsHelper)
        {
            _configurationsHelper = configurationsHelper;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var apiKey = _configurationsHelper.SendGrid_APIKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(_configurationsHelper.SendGrid_FromEmailAddress, _configurationsHelper.SendGrid_FromEmailAddressName);
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);

                await client.SendEmailAsync(msg);
            }
            catch (Exception)
            {
                // Log the error instead of failing silently
                Console.WriteLine("Failed to send email.");
            }
        }

        public async Task SendToEmailAsync(string fromEmailAddressName, string fromEmailAddress, string toEmailAddress, string toEmailSubject, string toEmailBody)
        {
            try
            {
                var apiKey = _configurationsHelper.SendGrid_APIKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmailAddress, fromEmailAddressName);
                var to = new EmailAddress(toEmailAddress);
                var msg = MailHelper.CreateSingleEmail(from, to, toEmailSubject, toEmailBody, toEmailBody);

                await client.SendEmailAsync(msg);
            }
            catch (Exception)
            {
                // Log the error instead of failing silently
                Console.WriteLine("Failed to send email.");
            }
        }
    }
}
