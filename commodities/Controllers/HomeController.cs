using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using commodities.Models;
using commodities.ApiControllers;

namespace commodities.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult GetRoot()
        {
            Node result = new CommoditiesController().GetNode().Data;
            return View(result);
        }
    }
}
