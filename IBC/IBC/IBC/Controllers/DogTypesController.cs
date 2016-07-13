using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IBC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using IBC.Filters;

namespace IBC.Controllers
{
    [Authorize]
    [CadastroCompletoFilter]
    public class DogTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DogTypees
        
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

            var retData = from d in db.DogTypes
                          select d;

            if (!string.IsNullOrEmpty(SearchString))
            {
                retData = from d in db.DogTypes
                          where d.Description.Contains(SearchString)
                          select d;
            }

            switch (ColumnName)
            {
                case "Description":
                    if (Convert.ToBoolean(Ascendent))
                    {
                        retData = retData.OrderBy(d => d.Description);
                    }
                    else
                    {
                        retData = retData.OrderByDescending(d => d.Description);
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
                    retData = retData.OrderBy(d => d.Description);
                    break;
            }



            return View(retData.ToPagedList(pageNumber, pageSize));
        }

        // GET: DogTypees/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogType DogType = db.DogTypes.Find(id);
            if (DogType == null)
            {
                return HttpNotFound();
            }
            return View(DogType);
        }

        // GET: DogTypees/Create
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogTypees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create([Bind(Include = "Id,Description")]DogType DogType)
        {
            if (ModelState.IsValid)
            {
                DogType.Id = Guid.NewGuid();
                DogType.UserProcessingId = User.Identity.GetUserId();
                DogType.ChangeDate = DateTime.Now;
                DogType.CreationDate = DateTime.Now;
                db.DogTypes.Add(DogType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(DogType);
        }

        // GET: DogTypees/Edit/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogType DogType = db.DogTypes.Find(id);
            if (DogType == null)
            {
                return HttpNotFound();
            }
            return View(DogType);
        }

        // POST: DogTypees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit([Bind(Include = "Id,Description,ChangeDate,CreationDate")]DogType DogType)
        {
            if (ModelState.IsValid)
            {
                DogType.UserProcessingId = User.Identity.GetUserId();
                DogType.ChangeDate = DateTime.Now;
                db.Entry(DogType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DogType);
        }

        // GET: DogTypees/Delete/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogType DogType = db.DogTypes.Find(id);
            if (DogType == null)
            {
                return HttpNotFound();
            }
            return View(DogType);
        }

        // POST: DogTypees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DogType DogType = db.DogTypes.Find(id);
            db.DogTypes.Remove(DogType);
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
