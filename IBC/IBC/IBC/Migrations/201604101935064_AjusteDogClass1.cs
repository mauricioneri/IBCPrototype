namespace IBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteDogClass1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUserExt", "Cidade", c => c.String(nullable: false, maxLength: 80));
            DropColumn("dbo.ApplicationUserExt", "Cidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUserExt", "Cidade", c => c.String(nullable: false, maxLength: 80));
            DropColumn("dbo.ApplicationUserExt", "Cidade");
        }
    }
}
