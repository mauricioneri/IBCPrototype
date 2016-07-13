using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
using IBC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace IBC.Helpers
{
    public static class IBCUserHelper
    {
        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
        }

        public static bool IsInAnyRole(this IPrincipal user, IList<string> roles)
        {
            bool retorno = false;
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();

                List<string> rolesForUser = new List<string>();
                using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {

                    rolesForUser = userManager.GetRoles(user.Identity.GetUserId()).ToList();
                }

                /*
                var userRoles = Roles.GetRolesForUser(user.Identity.Name);*/
                retorno = rolesForUser.Any(u => roles.Contains(u));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
            return retorno;
        }
        public static bool IsInAnyRole(this IPrincipal user, string roles)
        {
            List<string> lstRoles = roles.Split(',').ToList();
            return IsInAnyRole(user, lstRoles);
        }
    }
}