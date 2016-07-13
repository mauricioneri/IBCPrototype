namespace IBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultEnrollmentPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contest", "DefaultEnrollmentPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contest", "DefaultEnrollmentPrice");
        }
    }
}
