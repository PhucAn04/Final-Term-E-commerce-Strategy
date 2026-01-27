using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Fake Email Sent to {email}: {subject} - {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
