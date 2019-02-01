namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSurname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Surname");
        }
    }
}
