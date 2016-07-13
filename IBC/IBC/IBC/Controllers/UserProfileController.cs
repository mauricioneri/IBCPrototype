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
using IBC.Helpers;

namespace IBC.Controllers
{
    [Authorize]
    public class UserProfileController : BaseController 
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserProfile
       
        public ActionResult Index(string ColumnName, bool? Ascendent, string CurrentFilter, string SearchString, int? page, int? PageSize)
        {
            if (string.IsNullOrEmpty(ColumnName))
            {
                ColumnName = "Name";
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

            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                userId = (new Guid()).ToString();
            }
            
            var retData = db.UserExt.Include(a => a.User);
            if (!User.IsInAnyRole("Admin,MasterMTF"))
            {
                retData = db.UserExt.Where(u => u.UserId.ToString() == userId).Include(a => a.User);
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                retData = db.UserExt.Where(u => u.Name.Contains(SearchString)).Include(a => a.User);
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

        // GET: UserProfile/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserExt applicationUserExt = db.UserExt.Find(id);
            if (applicationUserExt == null)
            {
                return HttpNotFound();
            }
            return View(applicationUserExt);
        }

        // GET: UserProfile/Create
        public ActionResult Create(string ActionName, string ControllerName)
        {
            ApplicationUserExt applicationUserExt = new ApplicationUserExt();
            applicationUserExt.UserId = User.Identity.GetUserId();
            applicationUserExt.UserProcessingId = User.Identity.GetUserId();
            applicationUserExt.ActionName = ActionName;
            applicationUserExt.ControllerName = ControllerName;

            return View(applicationUserExt);
        }


        // POST: UserProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Name,DataDeNascimento,Endereco,Numero,Complemento,Bairro,Cidade,Estado,CEP,TelefoneFixo,TelefoneCelular,NaoPossuiCPF,CPF,NaoPossuiRG,RG,OutroDocumento,OutroDocumentoDescription,OutroDocumentoNumero,UserProcessingId,CreationDate,ChangeDate,ActionName,ControllerName")] ApplicationUserExt applicationUserExt)
        {
            if (ModelState.IsValid)
            {

                applicationUserExt.UserProcessingId = User.Identity.GetUserId();
                applicationUserExt.ChangeDate = DateTime.Now;
                applicationUserExt.CreationDate = DateTime.Now;

                db.UserExt.Add(applicationUserExt);
                db.SaveChanges();
                return RedirectToAction(applicationUserExt.ActionName, applicationUserExt.ControllerName);
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", applicationUserExt.UserId);
            return View(applicationUserExt);
        }



        // GET: UserProfile/Edit/5
        [CadastroCompletoFilter]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserExt applicationUserExt = db.UserExt.Find(id);
            if (applicationUserExt == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", applicationUserExt.UserId);
            return View(applicationUserExt);
        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,DataDeNascimento,Endereco,Numero,Complemento,Bairro,Cidade,Estado,CEP,TelefoneFixo,TelefoneCelular,NaoPossuiCPF,CPF,NaoPossuiRG,RG,OutroDocumento,OutroDocumentoDescription,OutroDocumentoNumero,UserProcessingId,CreationDate,ChangeDate")] ApplicationUserExt applicationUserExt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUserExt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", applicationUserExt.UserId);
            return View(applicationUserExt);
        }

        // GET: UserProfile/Delete/5

        [Authorize(Roles = "MasterMTF, Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserExt applicationUserExt = db.UserExt.Find(id);
            if (applicationUserExt == null)
            {
                return HttpNotFound();
            }
            return View(applicationUserExt);
        }

        // POST: UserProfile/Delete/5
        [Authorize(Roles = "MasterMTF, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUserExt applicationUserExt = db.UserExt.Find(id);
            db.UserExt.Remove(applicationUserExt);
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
