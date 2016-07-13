using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IBC.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IBC.Models.Views;
using IBC.Filters;

namespace IBC.Controllers
{
    [Authorize]
    [CadastroCompletoFilter]
    public class DogAgesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DogAges
        [Authorize(Roles = "Admin,MasterMTF")]

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

            var retData = from d in db.DogAges
                          select d;

            if (!string.IsNullOrEmpty(SearchString))
            {
                retData = from d in db.DogAges
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


        // GET: DogAges/Details/5
        [Authorize(Roles = "Admin,MasterMTF")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogAge DogAge = db.DogAges.Find(id);
            if (DogAge == null)
            {
                return HttpNotFound();
            }
            return View(DogAge);
        }

        // GET: DogAges/Create
        [Authorize(Roles = "Admin,MasterMTF")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogAges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MasterMTF")]
        public ActionResult Create([Bind(Include = "Id,Description,StartMonth,EndMonth,ChangeDate")] DogAge DogAge)
        {
            if (ModelState.IsValid)
            {
                DogAge.Id = Guid.NewGuid();
                DogAge.UserProcessingId = User.Identity.GetUserId();
                DogAge.ChangeDate = DateTime.Now;
                DogAge.CreationDate = DateTime.Now;

                db.DogAges.Add(DogAge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(DogAge);
        }

        // GET: DogAges/Edit/5
        [Authorize(Roles = "Admin,MasterMTF")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogAge DogAge = db.DogAges.Find(id);
            if (DogAge == null)
            {
                return HttpNotFound();
            }
            return View(DogAge);
        }

        // POST: DogAges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MasterMTF")]
        public ActionResult Edit([Bind(Include = "Id,Description,StartMonth,EndMonth,CreationDate")] DogAge DogAge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(DogAge).State = EntityState.Modified;
                DogAge.UserProcessingId = User.Identity.GetUserId();
                DogAge.ChangeDate = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DogAge);
        }

        // GET: DogAges/Delete/5
        [Authorize(Roles = "Admin,MasterMTF")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogAge DogAge = db.DogAges.Find(id);
            if (DogAge == null)
            {
                return HttpNotFound();
            }
            return View(DogAge);
        }

        // POST: DogAges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MasterMTF")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DogAge DogAge = db.DogAges.Find(id);
            db.DogAges.Remove(DogAge);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IEnumerable<CheckBoxListView> ListAgeClass(Guid? id)
        {
            var da = db.DogAges.ToList().Select(a => new CheckBoxListView { Id = a.Id, Description = a.Description, State = false });

            var dc = db.DogClass.Include("DogAges").Where(c => c.Id == id).SelectMany(c => c.DogAges.Select(s => s.Id));

            var ages = from a in da
                       join c in dc
                       on a.Id equals c
                       into aa
                       from c in aa.DefaultIfEmpty()
                       select new CheckBoxListView
                       {
                           Id = a.Id,
                           Description = a.Description,
                           State = (c == new Guid("00000000-0000-0000-0000-000000000000") ? false : true)
                       };

            return ages;
        }

        public ActionResult getDogAges(Guid? Id)
        {
            List<DogAge> DogAge;

            if (Id.HasValue)
            {
                DogAge = db.DogAges.OrderBy(o => o.DisplayOrder).Where(s => s.Id == Id).ToList();
            }
            else
            {
                DogAge = db.DogAges.OrderBy(o => o.DisplayOrder).ToList();
            }
            return Json(DogAge, JsonRequestBehavior.AllowGet);
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
