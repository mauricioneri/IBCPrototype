/*
 * 
 * Enable-Migrations
 * Add-Migration "Name qualquer"
 * Update-database
 * 
 * */

using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace IBC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<MenuModel>()
            //    .Property(c => c.ParentId).IsOptional();

            // modelBuilder.Entity<MenuModel>()
            //     .HasMany(c => c.ChildMenu)
            //     .WithOptional(c => c.ParentId)
            //     .HasForeignKey(c => c.Id);


            modelBuilder.Entity<Dog>()
                .HasOptional(p => p.Father)
                .WithMany()
                .Map(p => p.MapKey("Pai"));

            modelBuilder.Entity<Dog>()
                .HasOptional(m => m.Mother)
                .WithMany()
                .Map(m => m.MapKey("Mae"));
            
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<IBC.Models.DogSex> DogSexes { get; set; }
        public System.Data.Entity.DbSet<IBC.Models.DogBreed> DogBreeds { get; set; }
        public System.Data.Entity.DbSet<IBC.Models.DogType> DogTypes { get; set; }
        public System.Data.Entity.DbSet<IBC.Models.ApplicationUserExt> UserExt { get; set; }
        public System.Data.Entity.DbSet<IBC.Models.DogCategory> DogCategories { get; set; }

        public System.Data.Entity.DbSet<IBC.Models.DogAge> DogAges { get; set; }

        public System.Data.Entity.DbSet<IBC.Models.DogClass> DogClass { get; set; }

        public System.Data.Entity.DbSet<IBC.Models.Dog> Dogs { get; set; }

        public System.Data.Entity.DbSet<IBC.Models.Contest> Contests { get; set; }

        public System.Data.Entity.DbSet<IBC.Models.ContestShelter> ContestShelthers{ get; set; }

        public System.Data.Entity.DbSet<IBC.Models.ContestShelterPrice> ContestSheltherPrices { get; set; }

        public System.Data.Entity.DbSet<IBC.Models.ContestEnrollment> ContestEnrollments { get; set; }

        public System.Data.Entity.DbSet<IBC.Models.ContestEnrollmentPrice> ContestEnrollmentPrices { get; set; }
  
    }
}