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
using IBC.Models.Views;
using PagedList;
using IBC.Filters;

namespace IBC.Controllers
{
    [Authorize]
    [CadastroCompletoFilter]
    public class DogClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DogClass
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

            var retData = from v in db.DogClass.Include(c => c.DogClassParent).Include(c => c.DogAges).ToList()
                          select new DogClassView
                          {
                              Id = v.Id,
                              Description = v.Description,
                              DogClassParentId = v.DogClassParentId,
                              DogClassParent = v.DogClassParent,
                              ChangeDate = v.ChangeDate,
                              CreationDate = v.CreationDate,
                              UserProcessingId = v.UserProcessingId,
                              DogAges = new List<CheckBoxListView>()
                          };




            if (!string.IsNullOrEmpty(SearchString))
            {
                retData = from v in db.DogClass.Include(c => c.DogClassParent).Include(c => c.DogAges).ToList()
                          where v.Description.Contains(SearchString)
                          select new DogClassView
                                     {
                                         Id = v.Id,
                                         Description = v.Description,
                                         DogClassParentId = v.DogClassParentId,
                                         DogClassParent = v.DogClassParent,
                                         ChangeDate = v.ChangeDate,
                                         CreationDate = v.CreationDate,
                                         UserProcessingId = v.UserProcessingId,
                                         DogAges = new List<CheckBoxListView>()
                                     };


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
        // GET: DogClass/Details/5
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogClass DogClass = db.DogClass.Include("DogAges").FirstOrDefault(i => i.Id == id);
            if (DogClass == null)
            {
                return HttpNotFound();
            }

            DogClassView cv = new DogClassView
            {
                Id = DogClass.Id,
                Description = DogClass.Description,
                DogClassParentId = DogClass.DogClassParentId,
                DogClassParent = DogClass.DogClassParent,
                ChangeDate = DogClass.ChangeDate,
                CreationDate = DogClass.CreationDate,
                UserProcessingId = DogClass.UserProcessingId
            };

            cv.DogAges = (from a in db.DogAges.ToList()
                          join s in DogClass.DogAges.ToList()
                          on a.Id equals s.Id into j
                          from subset in j.DefaultIfEmpty()
                          select new CheckBoxListView
                          {
                              Id = a.Id,
                              Description = a.Description,
                              State = (subset == null ? false : true)
                          }).ToList();
            return View(cv);
        }

        // GET: DogClass/Create
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Create()
        {
            ViewBag.DogClassParentId = new SelectList(db.DogClass, "Id", "Description");
            DogClassView cv = new DogClassView();
            cv.DogAges = (from c in db.DogAges
                          select new CheckBoxListView { Id = c.Id, Description = c.Description, State = false }).ToList();
            return View(cv);
        }

        // POST: DogClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Create([Bind(Include = "Id,Description,DogClassParentId,DogAges")] DogClassView DogClass)
        {
            if (ModelState.IsValid)
            {
                DogClass DogClassModel = new DogClass
                                     {
                                         Id = Guid.NewGuid(),
                                         Description = DogClass.Description,
                                         DogClassParentId = DogClass.DogClassParentId,
                                         DogClassParent = DogClass.DogClassParent
                                     };

                DogClassModel.ChangeDate = DateTime.Now;
                DogClassModel.CreationDate = DateTime.Now;
                DogClassModel.UserProcessingId = User.Identity.GetUserId();

                var selected = from s in DogClass.DogAges
                               where s.State == true
                               select s;

                if (DogClass.DogAges != null)
                {
                    foreach (var item in selected)
                    {

                        var Ages = db.DogAges.Find(item.Id);
                        DogClassModel.DogAges.Add(Ages);
                    }
                }
                db.DogClass.Add(DogClassModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DogClassParentId = new SelectList(db.DogClass, "Id", "Description", DogClass.DogClassParentId);
            return View(DogClass);
        }

        // GET: DogClass/Edit/5
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogClass DogClass = db.DogClass.Find(id);
            if (DogClass == null)
            {
                return HttpNotFound();
            }
            DogClassView cv = new DogClassView
            {
                Id = DogClass.Id,
                Description = DogClass.Description,
                DogClassParentId = DogClass.DogClassParentId,
                DogClassParent = DogClass.DogClassParent,
                ChangeDate = DogClass.ChangeDate,
                CreationDate = DogClass.CreationDate,
                UserProcessingId = DogClass.UserProcessingId,
                DogAges = new List<CheckBoxListView>()
            };
            //cv.DogAges = (from a in db.DogAges.ToList()
            //              join s in DogClass.DogAges.ToList()
            //              on a.Id equals s.Id into j
            //              from subset in j.DefaultIfEmpty()
            //              select new CheckBoxListView
            //              {
            //                  Id = a.Id,
            //                  Description = a.Description,
            //                  State = (subset == null ? false : true)
            //              }).ToList();

            DogAgesController dac = new DogAgesController();
            cv.DogAges = dac.ListAgeClass(cv.Id).ToList();


            ViewBag.DogClassParentId = new SelectList(db.DogClass, "Id", "Description", DogClass.DogClassParentId);
            return View(cv);
        }

        // POST: DogClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Edit([Bind(Include = "Id,Description,DogClassParentId,DogAges,CreationDate")] DogClassView DogClass)
        {
            if (ModelState.IsValid)
            {
                DogClass DogClassModel = db.DogClass.Include("DogAges").FirstOrDefault(c => c.Id == DogClass.Id);
                if (DogClassModel == null)
                {
                    return HttpNotFound();
                }
                DogClassModel.Description = DogClass.Description;
                DogClassModel.DogClassParentId = DogClass.DogClassParentId;
                DogClassModel.DogClassParent = DogClass.DogClassParent;
                DogClassModel.ChangeDate = DateTime.Now;
                DogClassModel.CreationDate = DateTime.Now;
                DogClassModel.UserProcessingId = User.Identity.GetUserId();
                DogClassModel.DogAges.Clear();
                var selected = from s in DogClass.DogAges
                               where s.State == true
                               select s;

                if (DogClass.DogAges != null)
                {
                    foreach (var item in selected)
                    {

                        var Ages = db.DogAges.Find(item.Id);
                        DogClassModel.DogAges.Add(Ages);
                    }
                }

                db.Entry(DogClassModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DogClassParentId = new SelectList(db.DogClass, "Id", "Description", DogClass.DogClassParentId);
            return View(DogClass);
        }

        // GET: DogClass/Delete/5
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogClass DogClass = db.DogClass.Find(id);
            if (DogClass == null)
            {
                return HttpNotFound();
            }
            DogClassView cv = new DogClassView
            {
                Id = DogClass.Id,
                Description = DogClass.Description,
                DogClassParentId = DogClass.DogClassParentId,
                DogClassParent = DogClass.DogClassParent,
                ChangeDate = DogClass.ChangeDate,
                CreationDate = DogClass.CreationDate,
                UserProcessingId = DogClass.UserProcessingId,
                DogAges = new List<CheckBoxListView>()
            };

            cv.DogAges = (from a in db.DogAges.ToList()
                          join s in DogClass.DogAges.ToList()
                          on a.Id equals s.Id into j
                          from subset in j.DefaultIfEmpty()
                          select new CheckBoxListView
                          {
                              Id = a.Id,
                              Description = a.Description,
                              State = (subset == null ? false : true)
                          }).ToList();



            return View(cv);
        }

        // POST: DogClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DogClass DogClass = db.DogClass.Find(id);
            db.DogClass.Remove(DogClass);
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
