using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareTrader.Models
{
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult Shares()
        {
            return View();
        }

        public ActionResult Brokers()
        {
            return View();
        }

        public ActionResult Announcements()
        {
            return View();
        }

        public ActionResult Trades()
        {
            return View();
        }

        public ActionResult InterestedShares()
        {
            return View();
        }
    }
}