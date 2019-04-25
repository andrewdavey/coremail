using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Emails;
using Emails.Views;
using Microsoft.AspNetCore.Mvc;
using WebApp.NetCore.Models;

namespace WebApp.NetCore.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var emailer = new Emails.Mailer();
            var str = await emailer.SendTestEmail("test@example.com", "Subject line", new TestViewData
            {
                Message = "Hi from Webapp.NetCore"
            });

            return Content(str);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
