namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Gender");
        }
    }
}
