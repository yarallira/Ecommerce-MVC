using ECommerce.Classes;
using ECommerce.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class CompaniesController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Companies
        public ActionResult Index()
        {
            IQueryable<Company> companies = db.Companies.Include(c => c.Cities).Include(c => c.Departaments);
            return View(companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // UPLOADS DE IMAGENS 

        //CONTROLE DE LIST VIEW EM CASCATA

        public JsonResult GetCities(int DepartamentsID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(m => m.DepartamentsID == DepartamentsID);
            return Json(cities);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(CombosHelper.GetCities(), "CityID", "Name");
            ViewBag.DepartamentsID = new SelectList(CombosHelper.GetDepartaments(), "DepartamentsID", "Name");
            return View();
        }

        // POST: Companies/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {             
                db.Companies.Add(company);
                db.SaveChanges();

                if (company.LogoFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}.jpg", company.CompanyID);

                    var response = FilesHelper.UploadPhoto(company.LogoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        company.Logo = pic;
                        //db.Entry(company).State = EntityState.Modified;
                        //db.SaveChanges();
                    }                                   
                }
                return RedirectToAction("Index");            
            }

            ViewBag.CityID = new SelectList(CombosHelper.GetCities(), "CityID", "Name", company.CityID);
            ViewBag.DepartamentsID = new SelectList(CombosHelper.GetDepartaments(), "DepartamentsID", "Name", company.DepartamentsID);
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(CombosHelper.GetCities(), "CityID", "Name", company.CityID);
            ViewBag.DepartamentsID = new SelectList(CombosHelper.GetDepartaments(), "DepartamentsID", "Name", company.DepartamentsID);
            return View(company);
        }

        // POST: Companies/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.LogoFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}.jpg", company.CompanyID);

                    var response = FilesHelper.UploadPhoto(company.LogoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        company.Logo = pic;
                    }
                }

                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(CombosHelper.GetCities(), "CityID", "Name", company.CityID);
            ViewBag.DepartamentsID = new SelectList(CombosHelper.GetDepartaments(), "DepartamentsID", "Name", company.DepartamentsID);
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
