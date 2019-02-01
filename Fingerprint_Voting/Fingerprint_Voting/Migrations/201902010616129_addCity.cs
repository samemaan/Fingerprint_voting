namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "City");
        }
    }
}
