using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IBC.Models;
using IBC.Models.Views;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using IBC.Filters;

namespace IBC.Controllers
{
    [Authorize]
    [CadastroCompletoFilter]
    public class ContestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contests

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

            var retData = from d in db.Contests
                          select d;

            if (!string.IsNullOrEmpty(SearchString))
            {
                retData = from d in db.Contests
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

        // GET: Contests/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        // GET: Contests/Create
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,StartDate,EndDate,PublicationDate,EnrollmentStartDate,EnrollmentEndDate,Description,DefaultEnrollmentPrice,Opened2Enrollment,ContestResult,UserProcessingId,CreationDate,ChangeDate")] Contest contest)
        {
            if (ModelState.IsValid)
            {
                contest.Id = Guid.NewGuid();
                contest.ChangeDate = DateTime.Now;
                contest.CreationDate = contest.ChangeDate;
                contest.UserProcessingId = User.Identity.GetUserId();
                db.Contests.Add(contest);

                foreach (var item in db.DogClass)
                {
                    ContestEnrollmentPrice cep = new ContestEnrollmentPrice
                    {
                        ContestId = contest.Id,
                        DogClassId = item.Id,
                        EndDate = contest.StartDate,
                        Id = Guid.NewGuid(),
                        Price = contest.DefaultEnrollmentPrice,
                        StartDate = contest.PublicationDate,
                        UserProcessingId = contest.UserProcessingId,
                        CreationDate = contest.CreationDate,
                        ChangeDate = contest.ChangeDate
                    };

                    db.ContestEnrollmentPrices.Add(cep);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contest);
        }

        // GET: Contests/Edit/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }

            return View(contest);
        }

        // POST: Contests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,StartDate,EndDate,PublicationDate,EnrollmentStartDate,EnrollmentEndDate,Description,DefaultEnrollmentPrice,Opened2Enrollment,ContestResult,UserProcessingId,CreationDate,ChangeDate")] Contest contest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contest).State = EntityState.Modified;
                contest.UserProcessingId = User.Identity.GetUserId();
                contest.ChangeDate = DateTime.Now;
                var prices = db.ContestEnrollmentPrices.Where(p => p.ContestId == contest.Id).Select(s => s.DogClassId).ToList();

                var col = db.DogClass.Where(c => !prices.Contains(c.Id));

                foreach (var item in col)
                {
                    ContestEnrollmentPrice cep = new ContestEnrollmentPrice
                    {
                        ContestId = contest.Id,
                        DogClassId = item.Id,
                        EndDate = contest.StartDate,
                        Id = Guid.NewGuid(),
                        Price = contest.DefaultEnrollmentPrice,
                        StartDate = contest.PublicationDate,
                        UserProcessingId = contest.UserProcessingId,
                        CreationDate = contest.ChangeDate,
                        ChangeDate = contest.ChangeDate
                    };

                    db.ContestEnrollmentPrices.Add(cep);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contest);
        }

        // GET: Contests/Delete/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        // POST: Contests/Delete/5
        [Authorize(Roles = "MasterMTF, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Contest contest = db.Contests.Find(id);
            db.Contests.Remove(contest);
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
