namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCity1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
