using FluentEmail.Core;
using System.Threading.Tasks;
using FluentEmail.Core.Interfaces;
namespace TrustWaveCarca.Services.Emailservices
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            await _fluentEmail
                .To(to)
                .Subject(subject)
                .Body(body, isHtml: true)  // Ensure HTML formatting
                .SendAsync();
        }
    }
}
