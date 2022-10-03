using System.Threading.Tasks;
using Crm.Application.Configuration.Emails;

namespace Crm.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            // Integration with email service.

            return;
        }
    }
}