using Emails;
using Emails.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {                      
            var emailer = new Emails.Mailer();
            await emailer.SendTestEmail("test@example.com", "Subject line", new TestViewData
            {
                Message = "Hi from .net core Console"
            });

            Console.WriteLine("Done.");
            Console.WriteLine("Hit Enter...");
            Console.ReadLine();
        }

        static void ConfigureServices(IServiceCollection services)
        {

        }

    }
}
