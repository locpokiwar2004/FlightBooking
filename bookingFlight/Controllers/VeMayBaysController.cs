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
    public class VeMayBaysController : Controller
    {
        private SellingTicketEntities db = new SellingTicketEntities();

        // GET: VeMayBays
        public ActionResult Index()
        {
            var veMayBays = db.VeMayBays.Include(v => v.ChuyenBay).Include(v => v.KhachHang);
            return View(veMayBays.ToList());
        }

        // GET: VeMayBays/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeMayBay veMayBay = db.VeMayBays.Find(id);
            if (veMayBay == null)
            {
                return HttpNotFound();
            }
            return View(veMayBay);
        }

        // GET: VeMayBays/Create
        public ActionResult Create()
        {
            ViewBag.MaCB = new SelectList(db.ChuyenBays, "MaCB", "MaSB_Di");
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen");
            return View();
        }

        // POST: VeMayBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

            ViewBag.MaCB = new SelectList(db.ChuyenBays, "MaCB", "MaSB_Di", veMayBay.MaCB);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", veMayBay.MaKH);
            return View(veMayBay);
        }

        // GET: VeMayBays/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeMayBay veMayBay = db.VeMayBays.Find(id);
            if (veMayBay == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCB = new SelectList(db.ChuyenBays, "MaCB", "MaSB_Di", veMayBay.MaCB);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", veMayBay.MaKH);
            return View(veMayBay);
        }

        // POST: VeMayBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVe,MaCB,MaKH,NgayDat,GiaVe")] VeMayBay veMayBay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veMayBay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCB = new SelectList(db.ChuyenBays, "MaCB", "MaSB_Di", veMayBay.MaCB);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", veMayBay.MaKH);
            return View(veMayBay);
        }

        // GET: VeMayBays/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeMayBay veMayBay = db.VeMayBays.Find(id);
            if (veMayBay == null)
            {
                return HttpNotFound();
            }
            return View(veMayBay);
        }

        // POST: VeMayBays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VeMayBay veMayBay = db.VeMayBays.Find(id);
            db.VeMayBays.Remove(veMayBay);
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
    }
}
