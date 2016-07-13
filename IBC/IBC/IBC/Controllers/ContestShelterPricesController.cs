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
    public class ContestShelterPricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContestShelterPrices
        //public ActionResult Index(Guid? id, string ColumnName, bool? Ascendent, string CurrentFilter, string SearchString, int? page, int? PageSize)
        //{

        //    if (string.IsNullOrEmpty(ColumnName))
        //    {
        //        ColumnName = "Description";
        //        Ascendent = true;
        //    }


        //    if (SearchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        SearchString = CurrentFilter;
        //    }

        //    Contest contest = db.Contests.Find(id);
        //    if (contest == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }


        //    int pageSize = (PageSize ?? 10);
        //    int pageNumber = (page ?? 1);

        //    ViewBag.ColumnName = ColumnName;
        //    ViewBag.Ascendent = Ascendent;
        //    ViewBag.CurrentFilter = CurrentFilter;
        //    ViewBag.PageSize = PageSize;
        //    ViewBag.ContextId = contest.Id;
        //    ViewBag.ContextName = contest.Name;

        //    var contestSheltherPrices = db.ContestSheltherPrices
        //     .Include(c => c.Contest).Include(c => c.UserProcessing)
        //     .Where(p => p.ContestId == id);

        //}

        // GET: ContestShelterPrices/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestShelterPrice contestShelterPrice = db.ContestSheltherPrices.Find(id);
            if (contestShelterPrice == null)
            {
                return HttpNotFound();
            }
            return View(contestShelterPrice);
        }

        // GET: ContestShelterPrices/Create
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create()
        {
          
            return View();
        }

        // POST: ContestShelterPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Create([Bind(Include = "Id,ContestShelterId,StartDate,EndDate,Address,Price,UserProcessingId,CreationDate,ChangeDate")] ContestShelterPrice contestShelterPrice)
        {
            if (ModelState.IsValid)
            {
                contestShelterPrice.Id = Guid.NewGuid();
                db.ContestSheltherPrices.Add(contestShelterPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          
            return View(contestShelterPrice);
        }

        // GET: ContestShelterPrices/Edit/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestShelterPrice contestShelterPrice = db.ContestSheltherPrices.Find(id);
            if (contestShelterPrice == null)
            {
                return HttpNotFound();
            }
            return View(contestShelterPrice);
        }

        // POST: ContestShelterPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Edit([Bind(Include = "Id,ContestShelterId,StartDate,EndDate,Address,Price,UserProcessingId,CreationDate,ChangeDate")] ContestShelterPrice contestShelterPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contestShelterPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
              return View(contestShelterPrice);
        }

        // GET: ContestShelterPrices/Delete/5
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestShelterPrice contestShelterPrice = db.ContestSheltherPrices.Find(id);
            if (contestShelterPrice == null)
            {
                return HttpNotFound();
            }
            return View(contestShelterPrice);
        }

        // POST: ContestShelterPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ContestShelterPrice contestShelterPrice = db.ContestSheltherPrices.Find(id);
            db.ContestSheltherPrices.Remove(contestShelterPrice);
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
