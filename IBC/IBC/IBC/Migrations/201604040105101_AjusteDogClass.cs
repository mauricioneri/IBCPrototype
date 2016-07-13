namespace IBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteDogClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DogClass", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DogClass", "Description", c => c.String());
        }
    }
}
