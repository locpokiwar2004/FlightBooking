using System.Linq;
using System.Web.Mvc;
using bookingFlight.Models;

namespace bookingFlight.Controllers
{
    public class Admin_Controller : Controller
    {
        private SellingTicketEntities db = new SellingTicketEntities();

        // GET: Admin_/Index
        public ActionResult Index()
        {
            // Check if admin is authenticated
            if (Session["AdminName"] == null)
            {
                return RedirectToAction("Login", "Admin_");
            }

            
            var admins = db.Admin_.ToList(); 
            return View(admins);
        }

        // GET: Admin_/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin_/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin_ admin)
        {
            // Example logic to verify admin credentials
            var adminFromDb = db.Admin_.FirstOrDefault(a => a.Name == admin.Name && a.MatKhau == admin.MatKhau);
            if (adminFromDb != null)
            {
               
                Session["AdminName"] = adminFromDb.Name;

                return RedirectToAction("Index", "Admin_");
            }

            // If credentials are not correct, show error
            ViewBag.Error = "Invalid admin credentials.";
            return View(admin);
        }

        // GET: Admin_/Logout
        public ActionResult Logout()
        {
            Session["AdminName"] = null; // Clear admin session
            return RedirectToAction("Index", "Home"); // Redirect to home or login page
        }
    }
}