using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emails.Views;

namespace ConsoleApp.NetFramework
{
    class Program
    {
        static async Task Main(string[] args)
        {            
            var emailer = new Emails.Mailer();
            await emailer.SendTestEmail("test@example.com", "Subject line", new TestViewData
            {
                Message = "Hi from .Net framework console"
            });

            Console.WriteLine("Done.");
            Console.WriteLine("Hit Enter...");
            Console.ReadLine();
        }
    }
}
