using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bookingFlight.Models;

namespace bookingFlight.Controllers
{
    public class ChuyenBaysController : Controller
    {
        private SellingTicketEntities db = new SellingTicketEntities();

        // GET: ChuyenBays
        [Authorize]
        public ActionResult Index()
        {
            var chuyenBays = db.ChuyenBays.Include(c => c.SanBay).Include(c => c.SanBay1);
            return View(chuyenBays.ToList());
        }

        // GET: ChuyenBays/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenBay chuyenBay = db.ChuyenBays.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(chuyenBay);
        }

        // GET: ChuyenBays/Create
        public ActionResult Create()
        {
            ViewBag.MaSB_Den = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.MaSB_Di = new SelectList(db.SanBays, "MaSB", "TenSB");
            return View();
        }

        // POST: ChuyenBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCB,MaSB_Di,MaSB_Den,NgayGioDi,NgayGioDen")] ChuyenBay chuyenBay)
        {
            if (ModelState.IsValid)
            {
                db.ChuyenBays.Add(chuyenBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSB_Den = new SelectList(db.SanBays, "MaSB", "TenSB", chuyenBay.MaSB_Den);
            ViewBag.MaSB_Di = new SelectList(db.SanBays, "MaSB", "TenSB", chuyenBay.MaSB_Di);
            return View(chuyenBay);
        }

        // GET: ChuyenBays/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenBay chuyenBay = db.ChuyenBays.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSB_Den = new SelectList(db.SanBays, "MaSB", "TenSB", chuyenBay.MaSB_Den);
            ViewBag.MaSB_Di = new SelectList(db.SanBays, "MaSB", "TenSB", chuyenBay.MaSB_Di);
            return View(chuyenBay);
        }

        // POST: ChuyenBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCB,MaSB_Di,MaSB_Den,NgayGioDi,NgayGioDen")] ChuyenBay chuyenBay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuyenBay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSB_Den = new SelectList(db.SanBays, "MaSB", "TenSB", chuyenBay.MaSB_Den);
            ViewBag.MaSB_Di = new SelectList(db.SanBays, "MaSB", "TenSB", chuyenBay.MaSB_Di);
            return View(chuyenBay);
        }

        // GET: ChuyenBays/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenBay chuyenBay = db.ChuyenBays.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(chuyenBay);
        }

        // POST: ChuyenBays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChuyenBay chuyenBay = db.ChuyenBays.Find(id);
            db.ChuyenBays.Remove(chuyenBay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Authorize]
        public ActionResult SearchFlights(string from, string to, DateTime? departDateTime)
        {
            var flights = db.ChuyenBays
                            .Where(cb => (string.IsNullOrEmpty(from) || cb.MaSB_Di == from) &&
                                         (string.IsNullOrEmpty(to) || cb.MaSB_Den == to) &&
                                         (!departDateTime.HasValue || DbFunctions.TruncateTime(cb.NgayGioDi) == DbFunctions.TruncateTime(departDateTime)))
                            .Include(c => c.SanBay)
                            .Include(c => c.SanBay1)
                            .ToList();

            return View(flights);
        }
    }
}
