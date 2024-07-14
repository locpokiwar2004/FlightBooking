using bookingFlight.Models;
using System.Data.Entity.Validation;
using System;
using System.Linq;
using System.Web.Mvc;

namespace bookingFlight.Controllers
{
    public class AccountController : Controller
    {
        private SellingTicketEntities db = new SellingTicketEntities();

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHang model)
        {
            if (ModelState.IsValid)
            {
                var user = db.KhachHangs.FirstOrDefault(u => u.TaiKhoan == model.TaiKhoan && u.MatKhau == model.MatKhau);
                if (user != null)
                {
                    // Set the user in session (or use other authentication methods)
                    Session["UserID"] = user.MaKH.ToString();
                    Session["UserName"] = user.TaiKhoan.ToString();
                    // Redirect to Dashboard or Home page after successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Clear all session variables
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang model, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (model.MatKhau != ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                }
                else
                {
                    try
                    {
                        // Generate a random MaKH value
                        model.MaKH = Guid.NewGuid().ToString().Substring(0, 10);

                        // Ensure unique email and username
                        if (db.KhachHangs.Any(k => k.Email == model.Email))
                        {
                            ModelState.AddModelError("Email", "Email already exists.");
                        }
                        else if (db.KhachHangs.Any(k => k.TaiKhoan == model.TaiKhoan))
                        {
                            ModelState.AddModelError("TaiKhoan", "Username already exists.");
                        }
                        else
                        {
                            db.KhachHangs.Add(model);
                            db.SaveChanges();
                            return RedirectToAction("Login", "Account");
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Handle validation errors
                        var errorMessages = ex.EntityValidationErrors
                                              .SelectMany(e => e.ValidationErrors)
                                              .Select(e => e.ErrorMessage);

                        foreach (var errorMessage in errorMessages)
                        {
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                }
            }
            // Return the model with errors if any validation fails
            return View(model);
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
