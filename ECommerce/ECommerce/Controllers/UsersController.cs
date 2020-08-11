using ECommerce.Classes;
using ECommerce.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class UsersController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        //CONTROLE DE LIST VIEW EM CASCATA

        public JsonResult GetCities(int DepartamentsID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<City> cities = db.Cities.Where(m => m.DepartamentsID == DepartamentsID);
            return Json(cities);
        }

        // GET: Users
        public ActionResult Index()
        {
            IQueryable<User> users = db.Users.Include(u => u.Cities).Include(u => u.Company).Include(u => u.Departaments);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(CombosHelper.GetCities(), "CityID", "Name");
            ViewBag.CompanyID = new SelectList(CombosHelper.GetCompanys(), "CompanyID", "Name");
            ViewBag.DepartamentsID = new SelectList(CombosHelper.GetDepartaments(), "DepartamentsID", "Name");
            return View();
        }

        // POST: Users/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null &&
                       ex.InnerException.InnerException != null &&
                       ex.InnerException.InnerException.Message.Contains("_Index"))
                        {
                            ModelState.AddModelError(string.Empty, "Não é possivel inserir duas cidades com o mesmo nome.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, ex.Message);
                        }
                        return View(user);
                    }

                    if (user.PhotoFile != null)
                    {
                        string pic = string.Empty;
                        string folder = "~/Content/Users";
                        string file = string.Format("{0}.jpg", user.UserID);

                        bool response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                        if (response)
                        {
                            pic = string.Format("{0}/{1}", folder, file);
                            user.Photo = pic;
                            db.Entry(user).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(CombosHelper.GetCities(), "CityID", "Name", user.CityID);
            ViewBag.CompanyID = new SelectList(CombosHelper.GetCompanys(), "CompanyID", "Name", user.CompanyID);
            ViewBag.DepartamentsID = new SelectList(CombosHelper.GetDepartaments(), "DepartamentsID", "Name", user.DepartamentsID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(CombosHelper.GetCities(), "CityID", "Name", user.CityID);
            ViewBag.CompanyID = new SelectList(CombosHelper.GetCompanys(), "CompanyID", "Name", user.CompanyID);
            ViewBag.DepartamentsID = new SelectList(CombosHelper.GetDepartaments(), "DepartamentsID", "Name", user.DepartamentsID);
            return View(user);
        }

        // POST: Users/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (user.PhotoFile != null)
                    {
                        string pic = string.Empty;
                        string folder = "~/Content/Users";
                        string file = string.Format("{0}.jpg", user.UserID);

                        bool response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                        if (response)
                        {
                            pic = string.Format("{0}/{1}", folder, file);
                            user.Photo = pic;
                        }
                    }
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(CombosHelper.GetCities(), "CityID", "Name", user.CityID);
            ViewBag.CompanyID = new SelectList(CombosHelper.GetCompanys(), "CompanyID", "Name", user.CompanyID);
            ViewBag.DepartamentsID = new SelectList(CombosHelper.GetDepartaments(), "DepartamentsID", "Name", user.DepartamentsID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
