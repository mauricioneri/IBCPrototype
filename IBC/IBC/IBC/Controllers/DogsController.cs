using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IBC.Models;
using IBC.Helpers;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;
using IBC.Filters;


namespace IBC.Controllers
{
    [Authorize]
    [CadastroCompletoFilter]
    public class DogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dogs
        public ActionResult Index(/*IPrincipal usuario*/)
        {
            IQueryable<Dog> Dogs;
            if (User.IsInAnyRole("Admin,MasterMTF"))
            {
                Dogs = db.Dogs.Include(c => c.Category).Include(c => c.DogBreed).Include(c => c.DogSex);
            }
            else
            {
                string OwnerId = HttpContext.User.Identity.GetUserId();
                Dogs = db.Dogs
                    .Where(c => c.OwnerId == OwnerId)
                    .Include(c => c.Father)
                    .Include(c => c.Mother)
                    .Include(c => c.Category)
                    .Include(c => c.DogBreed)
                    .Include(c => c.DogSex);
            }
            return View(Dogs.ToList());
        }
        
        public ActionResult Index(string ColumnName, bool? Ascendent, string CurrentFilter, string SearchString, int? page, int? PageSize)
        {
            if (string.IsNullOrEmpty(ColumnName))
            {
                ColumnName = "Description";
                Ascendent = true;
            }


            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }

            int pageSize = (PageSize ?? 10);
            int pageNumber = (page ?? 1);

            ViewBag.ColumnName = ColumnName;
            ViewBag.Ascendent = Ascendent;
            ViewBag.CurrentFilter = CurrentFilter;
            ViewBag.PageSize = PageSize;

            var retData = from d in db.Dogs 
                          select d;

            if (!string.IsNullOrEmpty(SearchString))
            {
                retData = from d in db.Dogs 
                          where d.Name.Contains(SearchString)
                          select d;
            }

            switch (ColumnName)
            {
                case "Name":
                    if (Convert.ToBoolean(Ascendent))
                    {
                        retData = retData.OrderBy(d => d.Name);
                    }
                    else
                    {
                        retData = retData.OrderByDescending(d => d.Name);
                    }
                    break;
                case "ChangeDate":
                    if (Convert.ToBoolean(Ascendent))
                    {
                        retData = retData.OrderBy(d => d.ChangeDate);
                    }
                    else
                    {
                        retData = retData.OrderByDescending(d => d.ChangeDate);
                    }
                    break;
                default:
                    retData = retData.OrderBy(d => d.Name);
                    break;
            }



            return View(retData.ToPagedList(pageNumber, pageSize));
        }
        // GET: Dogs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog Dog = db.Dogs.Find(id);
            if (Dog == null)
            {
                return HttpNotFound();
            }
            return View(Dog);
        }

        // GET: Dogs/Create
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.DogCategories, "Id", "Description");
            ViewBag.DogBreedId = new SelectList(db.DogBreeds, "Id", "Description");
            ViewBag.DogSexId = new SelectList(db.DogSexes, "Id", "Description");
            return View();
        }

        // POST: Dogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create([Bind(Include = "Pedigree,Name,DtNascimento,PaiId,MaeId,DogSexId,DogBreedId,CategoryId,AtivoExposicao")] Dog Dog)
        {
            if (ModelState.IsValid)
            {
                Dog.Id = Guid.NewGuid();
                Dog.UserProcessingId = User.Identity.GetUserId();
                Dog.CreationDate = DateTime.Now;
                Dog.ChangeDate = DateTime.Now;
                db.Dogs.Add(Dog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.DogCategories, "Id", "Description", Dog.CategoryId);
            ViewBag.DogBreedId = new SelectList(db.DogBreeds, "Id", "Description", Dog.DogBreedId);
            ViewBag.DogSexId = new SelectList(db.DogSexes, "Id", "Description", Dog.DogSexId);
            return View(Dog);
        }

        // GET: Dogs/Edit/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog Dog = db.Dogs.Find(id);
            if (Dog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.DogCategories, "Id", "Description", Dog.CategoryId);
            ViewBag.DogBreedId = new SelectList(db.DogBreeds, "Id", "Description", Dog.DogBreedId);
            ViewBag.DogSexId = new SelectList(db.DogSexes, "Id", "Description", Dog.DogSexId);
            return View(Dog);
        }

        // POST: Dogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit([Bind(Include = "Id,Pedigree,Name,DtNascimento,PaiId,MaeId,DogSexId,DogBreedId,CategoryId,AtivoExposicao,CreationDate")] Dog Dog)
        {
            if (ModelState.IsValid)
            {
                Dog.UserProcessingId = User.Identity.GetUserId();
                Dog.ChangeDate = DateTime.Now;
                db.Entry(Dog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.DogCategories, "Id", "Description", Dog.CategoryId);
            ViewBag.DogBreedId = new SelectList(db.DogBreeds, "Id", "Description", Dog.DogBreedId);
            ViewBag.DogSexId = new SelectList(db.DogSexes, "Id", "Description", Dog.DogSexId);
            return View(Dog);
        }

        // GET: Dogs/Delete/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog Dog = db.Dogs.Find(id);
            if (Dog == null)
            {
                return HttpNotFound();
            }
            return View(Dog);
        }

        // POST: Dogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Dog Dog = db.Dogs.Find(id);
            db.Dogs.Remove(Dog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult getListaDogPaiMae(string Sigla)
        {

            Guid DogSexId;
            switch (Sigla.ToString())
            {
                case "M":
                    DogSexId = db.DogSexes.Where(s => s.Sigla == "M").Select(s => s.Id).FirstOrDefault();
                    break;
                case "F":
                    DogSexId = db.DogSexes.Where(s => s.Sigla == "F").Select(s => s.Id).FirstOrDefault();
                    break;
                default:
                    return Json("Valor inválido", JsonRequestBehavior.DenyGet);
            }
            var Dog = db.Dogs
                 .Where(c => c.DogSexId == DogSexId)
                 .Select(c => new { c.Id, Name = c.Name })
                 .ToList();
            return Json(Dog, JsonRequestBehavior.AllowGet);
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
