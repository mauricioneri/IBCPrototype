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
    public class ContestEnrollmentPricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContestEnrollmentPrices
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Index(Guid? id, string ColumnName, bool? Ascendent, string CurrentFilter, string SearchString, int? page, int? PageSize)
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

            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            int pageSize = (PageSize ?? 10);
            int pageNumber = (page ?? 1);

            ViewBag.ColumnName = ColumnName;
            ViewBag.Ascendent = Ascendent;
            ViewBag.CurrentFilter = CurrentFilter;
            ViewBag.PageSize = PageSize;
            ViewBag.ContextId = contest.Id;
            ViewBag.ContextName = contest.Name;

            var contestEnrollmentPrices = db.ContestEnrollmentPrices
                .Include(c => c.Contest).Include(c => c.DogClass).Include(c => c.UserProcessing)
                .Where(p => p.ContestId == id);


            var retData = from d in contestEnrollmentPrices
                          select d;

            if (!string.IsNullOrEmpty(SearchString))
            {
                retData = from d in contestEnrollmentPrices
                          where d.DogClass.Description.Contains(SearchString)
                          select d;
            }

            switch (ColumnName)
            {
                case "Description":
                    if (Convert.ToBoolean(Ascendent))
                    {
                        retData = retData.OrderBy(d => d.DogClass.Description);
                    }
                    else
                    {
                        retData = retData.OrderByDescending(d => d.DogClass.Description);

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
                    retData = retData.OrderBy(d => d.DogClass.Description);
                    break;
            }



            return View(retData.ToPagedList(pageNumber, pageSize));
        }



        // GET: ContestEnrollmentPrices/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestEnrollmentPrice contestEnrollmentPrice = db.ContestEnrollmentPrices.Find(id);
            if (contestEnrollmentPrice == null)
            {
                return HttpNotFound();
            }
            return View(contestEnrollmentPrice);
        }

        // GET: ContestEnrollmentPrices/Create
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contest c = db.Contests.Find(id);
            if (c == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ContextId = c.Id;
            ViewBag.ContextName = c.Name;
            ViewBag.DogClassId = new SelectList(db.DogClass, "Id", "Description");
            return View();
        }

        // POST: ContestEnrollmentPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create([Bind(Include = "Id,ContestId,DogClassId,StartDate,EndDate,Price")] ContestEnrollmentPrice contestEnrollmentPrice)
        {
            if (ModelState.IsValid)
            {
                contestEnrollmentPrice.Id = Guid.NewGuid();
                db.ContestEnrollmentPrices.Add(contestEnrollmentPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            Contest c = db.Contests.Find(contestEnrollmentPrice.ContestId);
            if (c == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ContextId = c.Id;
            ViewBag.ContextName = c.Name;
            ViewBag.DogClassId = new SelectList(db.DogClass, "Id", "Description", contestEnrollmentPrice.DogClassId);

            return View(contestEnrollmentPrice);
        }

        // GET: ContestEnrollmentPrices/Edit/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestEnrollmentPrice contestEnrollmentPrice = db.ContestEnrollmentPrices.Find(id);
            if (contestEnrollmentPrice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContestId = new SelectList(db.Contests, "Id", "Name", contestEnrollmentPrice.ContestId);
            ViewBag.DogClassId = new SelectList(db.DogClass, "Id", "Description", contestEnrollmentPrice.DogClassId);
            // ViewBag.UserProcessingId = new SelectList(db.ApplicationUsers, "Id", "Email", contestEnrollmentPrice.UserProcessingId);
            return View(contestEnrollmentPrice);
        }

        // POST: ContestEnrollmentPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit([Bind(Include = "Id,ContestId,DogClassId,StartDate,EndDate,Price,UserProcessingId,CreationDate,ChangeDate")] ContestEnrollmentPrice contestEnrollmentPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contestEnrollmentPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContestId = new SelectList(db.Contests, "Id", "Name", contestEnrollmentPrice.ContestId);
            ViewBag.DogClassId = new SelectList(db.DogClass, "Id", "Description", contestEnrollmentPrice.DogClassId);
            //ViewBag.UserProcessingId = new SelectList(db.ApplicationUsers, "Id", "Email", contestEnrollmentPrice.UserProcessingId);
            return View(contestEnrollmentPrice);
        }

        // GET: ContestEnrollmentPrices/Delete/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestEnrollmentPrice contestEnrollmentPrice = db.ContestEnrollmentPrices.Find(id);
            if (contestEnrollmentPrice == null)
            {
                return HttpNotFound();
            }
            return View(contestEnrollmentPrice);
        }

        // POST: ContestEnrollmentPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ContestEnrollmentPrice contestEnrollmentPrice = db.ContestEnrollmentPrices.Find(id);
            db.ContestEnrollmentPrices.Remove(contestEnrollmentPrice);
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
