namespace IBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Money1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contest", "DefaultEnrollmentPrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contest", "DefaultEnrollmentPrice");
        }
    }
}
