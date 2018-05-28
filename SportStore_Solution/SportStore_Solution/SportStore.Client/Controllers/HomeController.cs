using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Kids()
        {
            return View();
        }
        public ActionResult Men()
        {
            return View();
        }
        public ActionResult Women()
        {
            return View();
        }
        public ActionResult Shoes()
        {
            return View();
        }
        public ActionResult Index_products()
        {
            return View();
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
        public JsonResult TodaysRate()
        {
            var r = ConfigurationManager.AppSettings["rate"];
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}