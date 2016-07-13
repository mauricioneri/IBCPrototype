namespace IBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContextPrice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContestEnrollmentPrice", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ContestEnrollmentPrice", new[] { "Owner_Id" });
            DropColumn("dbo.ContestEnrollmentPrice", "OwnerId");
            DropColumn("dbo.ContestEnrollmentPrice", "Owner_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContestEnrollmentPrice", "Owner_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ContestEnrollmentPrice", "OwnerId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ContestEnrollmentPrice", "Owner_Id");
            AddForeignKey("dbo.ContestEnrollmentPrice", "Owner_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
