namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Country");
        }
    }
}
