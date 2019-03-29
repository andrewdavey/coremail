using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Emails.Views;

namespace Emails
{
    public class Mailer
    {
        readonly RazorViewToStringRenderer _renderer;

        public Mailer(RazorViewToStringRenderer renderer)
        {
            _renderer = renderer;
        }

        public async Task SendTestEmail(string to, string subject, TestViewData viewData)
        {
            var htmlBody = await _renderer.RenderViewToStringAsync("Test", viewData);

            // TODO: Inline CSS of the HTML body

            var message = new MailMessage("sender@example.com", to, subject, htmlBody);

            // TODO: send message

            Console.WriteLine(message.Body);
        }
    }
}
