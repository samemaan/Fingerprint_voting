namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFirstName1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
        }
    }
}
