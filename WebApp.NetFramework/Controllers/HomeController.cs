using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Emails.Views;

namespace WebApp.NetFramework.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var emailer = new Emails.Mailer();
            var str = await emailer.SendTestEmail("test@example.com", "Subject line", new TestViewData
            {
                Message = "Hi from WebApp.Netframework"
            });

            return Content(str);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}