using IBC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IBC.Filters
{
    public class CadastroCompletoFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string currentUser = HttpContext.Current.User.Identity.Name;
            string userId = HttpContext.Current.User.Identity.GetUserId();

            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!HttpContext.Current.User.IsInRole("MasterMTF") && !HttpContext.Current.User.IsInRole("Admin"))
            {
                ApplicationUserExt up = context.UserExt.FirstOrDefault(u => u.UserId == userId);
                if (up == null)
                {
                    var ActionName = filterContext.ActionDescriptor.ActionName;
                    var ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary{
                                {"controller","UserProfile"},
                                {"action", "Create"},
                                {"userId",userId},
                                {"ActionName",ActionName},
                                {"ControllerName",ControllerName}
                            });
                }
            }
        }
      
    }
}