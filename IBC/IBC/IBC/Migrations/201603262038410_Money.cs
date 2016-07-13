namespace IBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Money : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContestEnrollmentPrice", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.ContestEnrollment", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.ContestEnrollment", "PricePaid", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.ContestShelter", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.ContestShelterPrice", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.Contest", "DefaultEnrollmentPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contest", "DefaultEnrollmentPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.ContestShelterPrice", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.ContestShelter", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.ContestEnrollment", "PricePaid", c => c.Double(nullable: false));
            AlterColumn("dbo.ContestEnrollment", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.ContestEnrollmentPrice", "Price", c => c.Double(nullable: false));
        }
    }
}
