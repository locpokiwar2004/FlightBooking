using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using bookingFlight.Models;

namespace bookingFlight.Controllers
{
    public class VeMayBaysController : Controller
    {
        private SellingTicketEntities db = new SellingTicketEntities();

        // GET: VeMayBays/Create
        public ActionResult Create()
        {
            ViewBag.MaCB = new SelectList(db.ChuyenBays, "MaCB", "TenCB");
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            return View();
        }

        // POST: VeMayBays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaVe,MaCB,MaKH,NgayDat,GiaVe")] VeMayBay veMayBay)
        {
            if (ModelState.IsValid)
            {
                db.VeMayBays.Add(veMayBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCB = new SelectList(db.ChuyenBays, "MaCB", "TenCB", veMayBay.MaCB);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", veMayBay.MaKH);
            return View(veMayBay);
        }

        // GET: VeMayBays
        public ActionResult Index()
        {
            var veMayBays = db.VeMayBays.Include(v => v.ChuyenBay).Include(v => v.KhachHang);
            return View(veMayBays.ToList());
        }
    }
}
