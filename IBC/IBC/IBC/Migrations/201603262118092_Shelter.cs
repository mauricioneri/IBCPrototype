namespace IBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shelter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContestShelterPrice", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContestShelterPrice", "Address");
        }
    }
}
