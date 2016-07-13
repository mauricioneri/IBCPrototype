using IBC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC
{
    public class MasterUsersConfig
    {
        public static void ConfigUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User  
            if (!roleManager.RoleExists("MasterMTF"))
            {

                // first we create Admin rool 
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "MasterMTF";
                roleManager.Create(role);
            }
            //Here we create a Admin super user who will maintain the website                 

            if (UserManager.FindByName("root_shanu") == null)
            {
                var user = new ApplicationUser();


                //user.UserName = "root_shanu";
                user.Email = "info@stoneagebulls.com.br";
                user.UserName = user.Email;

                
                string userPWD = @"A@Z200711!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Master 
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "MasterMTF");

                }
            }
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool 
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("User"))
            {

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("User_Handler"))
            {

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User_Handler";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("User_Breeder"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User_Breeder";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("User_Referee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User_Referee";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("User_Assistant"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User_Assistant";
                roleManager.Create(role);
            }
        }
    }
}