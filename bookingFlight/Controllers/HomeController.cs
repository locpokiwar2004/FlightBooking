using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using bookingFlight.Models;
namespace bookingFlight.Controllers
{
    public class HomeController : Controller
    {
        SellingTicketEntities db = new SellingTicketEntities();
        public ActionResult Index()
        {
            ViewBag.MaSB_Di = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.MaSB_Den = new SelectList(db.SanBays, "MaSB", "TenSB");
            return View();
        }
        [HttpPost]
        public ActionResult SearchFlights(string from, string to, DateTime departDateTime)
        {
            var flights = db.ChuyenBays
                            .Where(cb => cb.MaSB_Di == from && cb.MaSB_Den == to && cb.NgayGioDi == departDateTime)
                            .ToList();

            return View(flights);
        }
    }
}