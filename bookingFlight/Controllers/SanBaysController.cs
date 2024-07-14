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
    public class SanBaysController : Controller
    {
        private SellingTicketEntities db = new SellingTicketEntities();

        // GET: SanBays
        public ActionResult Index()
        {
            return View(db.SanBays.ToList());
        }

        // GET: SanBays/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanBay sanBay = db.SanBays.Find(id);
            if (sanBay == null)
            {
                return HttpNotFound();
            }
            return View(sanBay);
        }

        // GET: SanBays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SanBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSB,TenSB,ThanhPho,QuocGia")] SanBay sanBay)
        {
            if (ModelState.IsValid)
            {
                db.SanBays.Add(sanBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanBay);
        }

        // GET: SanBays/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanBay sanBay = db.SanBays.Find(id);
            if (sanBay == null)
            {
                return HttpNotFound();
            }
            return View(sanBay);
        }

        // POST: SanBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSB,TenSB,ThanhPho,QuocGia")] SanBay sanBay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanBay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanBay);
        }

        // GET: SanBays/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanBay sanBay = db.SanBays.Find(id);
            if (sanBay == null)
            {
                return HttpNotFound();
            }
            return View(sanBay);
        }

        // POST: SanBays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanBay sanBay = db.SanBays.Find(id);
            db.SanBays.Remove(sanBay);
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
