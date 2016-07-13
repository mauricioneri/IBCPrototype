using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IBC.Models;
using IBC.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace IBC.Controllers
{

    [Authorize]
    [CadastroCompletoFilter]
    public class DogSexesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DogSexes
        [Authorize(Roles = "MasterMTF,Admin")]
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

            var retData = from d in db.DogSexes
                          select d;

            if (!string.IsNullOrEmpty(SearchString))
            {
                retData = from d in db.DogSexes
                          where d.Description.Contains(SearchString) || d.Sigla == SearchString
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

        // GET: DogSexes/Details/5
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogSex DogSex = db.DogSexes.Find(id);
            if (DogSex == null)
            {
                return HttpNotFound();
            }
            return View(DogSex);
        }

        // GET: DogSexes/Create
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogSexes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Create([Bind(Include = "Id,Description,Sigla")] DogSex DogSex)
        {
            if (ModelState.IsValid)
            {
                DogSex.Id = Guid.NewGuid();
                DogSex.UserProcessingId = User.Identity.GetUserId();
                DogSex.ChangeDate = DateTime.Now;
                DogSex.CreationDate = DateTime.Now;

                db.DogSexes.Add(DogSex);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(DogSex);
        }

        // GET: DogSexes/Edit/5
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogSex DogSex = db.DogSexes.Find(id);
            if (DogSex == null)
            {
                return HttpNotFound();
            }
            return View(DogSex);
        }

        // POST: DogSexes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Edit([Bind(Include = "Id,Description,Sigla,CreationDate")] DogSex DogSex)
        {
            if (ModelState.IsValid)
            {
                DogSex.UserProcessingId = User.Identity.GetUserId();
                DogSex.ChangeDate = DateTime.Now;
                DogSex.CreationDate = DateTime.Now;


                db.Entry(DogSex).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DogSex);
        }

        // GET: DogSexes/Delete/5
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogSex DogSex = db.DogSexes.Find(id);
            if (DogSex == null)
            {
                return HttpNotFound();
            }
            return View(DogSex);
        }

        // POST: DogSexes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DogSex DogSex = db.DogSexes.Find(id);
            db.DogSexes.Remove(DogSex);
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
