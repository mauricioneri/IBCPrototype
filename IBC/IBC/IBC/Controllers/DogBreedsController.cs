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
    public class DogBreedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DogBreeds
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

            var retData = (from r in db.DogBreeds.Include("Categories").ToList()
                           select new DogBreedView
                           {
                               Id = r.Id,
                               Description = r.Description,
                               ChangeDate = r.ChangeDate,
                               CreationDate = r.CreationDate,
                               UserProcessing = r.UserProcessing,
                               HasCategory = r.Categories.Count > 0 ? true : false
                           });


            if (!string.IsNullOrEmpty(SearchString))
            {
                retData = (from r in db.DogBreeds.Include("Categories").ToList()
                           where r.Description.Contains(SearchString)
                           select new DogBreedView
                           {
                               Id = r.Id,
                               Description = r.Description,
                               ChangeDate = r.ChangeDate,
                               CreationDate = r.CreationDate,
                               UserProcessing = r.UserProcessing,
                               HasCategory = r.Categories.Count > 0 ? true : false
                           });

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

        // GET: DogBreeds/Details/5
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogBreed DogBreed = db.DogBreeds.Include("Categories").FirstOrDefault(r => r.Id == id);
            if (DogBreed == null)
            {
                return HttpNotFound();
            }

            DogBreedView rv = new DogBreedView
            {
                Id = DogBreed.Id,
                Description = DogBreed.Description,
                ChangeDate = DogBreed.ChangeDate,
                CreationDate = DogBreed.CreationDate,
                UserProcessingId = DogBreed.UserProcessingId
            };
            rv.DogCategories = (from c in db.DogCategories.ToList()
                                join s in DogBreed.Categories.ToList()
                                on c.Id equals s.Id into j
                                from subset in j.DefaultIfEmpty()
                                select new CheckBoxListView
                                {
                                    Id = c.Id,
                                    Description = c.Description,
                                    State = (subset == null ? false : true)
                                }).ToList();
            return View(rv);
        }

        // GET: DogBreeds/Create
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Create()
        {
            DogBreedView rv = new DogBreedView();
            rv.DogCategories = (from c in db.DogCategories
                                select new CheckBoxListView { Id = c.Id, Description = c.Description, State = false }).ToList();

            return View(rv);
        }

        // POST: DogBreeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Create([Bind(Include = "Id,Description,UserProcessingId,CreationDate,ChangeDate,DogCategories")] DogBreedView DogBreed)
        {
            if (ModelState.IsValid)
            {
                DogBreed DogBreedModel = new DogBreed();
                DogBreedModel.Id = Guid.NewGuid();
                DogBreedModel.UserProcessingId = User.Identity.GetUserId();
                DogBreedModel.ChangeDate = DateTime.Now;
                DogBreedModel.CreationDate = DateTime.Now;

                DogBreedModel.Description = DogBreed.Description;

                var selected = from s in DogBreed.DogCategories
                               where s.State == true
                               select s;


                if (DogBreed.DogCategories != null)
                {
                    foreach (var item in selected)
                    {
                        var Category = db.DogCategories.Find(item.Id);
                        DogBreedModel.Categories.Add(Category);
                    }
                }

                db.DogBreeds.Add(DogBreedModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(DogBreed);
        }

        // GET: DogBreeds/Edit/5
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Edit(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogBreed DogBreed = db.DogBreeds.Include("Categories").FirstOrDefault(r => r.Id == id);
            if (DogBreed == null)
            {
                return HttpNotFound();
            }

            DogBreedView rv = new DogBreedView { Id = DogBreed.Id, Description = DogBreed.Description, ChangeDate = DogBreed.ChangeDate, CreationDate = DogBreed.CreationDate, UserProcessingId = DogBreed.UserProcessingId };

            rv.DogCategories = (from c in db.DogCategories.ToList()
                                join s in DogBreed.Categories.ToList()
                                on c.Id equals s.Id into g
                                from subset in g.DefaultIfEmpty()
                                select new CheckBoxListView
                                {
                                    Id = c.Id,
                                    Description = c.Description,
                                    State = (subset == null ? false : true)
                                }).ToList();


            return View(rv);
        }

        // POST: DogBreeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Edit([Bind(Include = "Id,Description,CreationDate,DogCategories")] DogBreedView DogBreed)
        {
            if (ModelState.IsValid)
            {

                DogBreed DogBreedModel = db.DogBreeds.Include("Categories").FirstOrDefault(r => r.Id == DogBreed.Id);
                DogBreedModel.UserProcessingId = User.Identity.GetUserId();
                DogBreedModel.ChangeDate = DateTime.Now;

                DogBreedModel.Description = DogBreed.Description;
                DogBreedModel.Categories.Clear();
                var selected = from s in DogBreed.DogCategories
                               where s.State == true
                               select s;


                if (DogBreed.DogCategories != null)
                {
                    foreach (var item in selected)
                    {
                        var Category = db.DogCategories.Find(item.Id);
                        DogBreedModel.Categories.Add(Category);
                    }
                }


                db.Entry(DogBreedModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DogBreed);
        }

        // GET: DogBreeds/Delete/5
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult Delete(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogBreed DogBreed = db.DogBreeds.Include("Categories").FirstOrDefault(r => r.Id == id);
            if (DogBreed == null)
            {
                return HttpNotFound();
            }

            DogBreedView rv = new DogBreedView { Id = DogBreed.Id, Description = DogBreed.Description, ChangeDate = DogBreed.ChangeDate, CreationDate = DogBreed.CreationDate, UserProcessingId = DogBreed.UserProcessingId };
            rv.DogCategories = (from c in db.DogCategories.ToList()
                                join s in DogBreed.Categories.ToList()
                                on c.Id equals s.Id into g
                                from subset in g.DefaultIfEmpty()
                                select new CheckBoxListView
                                {
                                    Id = c.Id,
                                    Description = c.Description,
                                    State = (subset == null ? false : true)
                                }).ToList();

            return View(rv);
        }

        // POST: DogBreeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MasterMTF,Admin")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DogBreed DogBreed = db.DogBreeds.Find(id);
            db.DogBreeds.Remove(DogBreed);
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
