namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImage2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UserPic", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UserPic", c => c.Binary(nullable: false, maxLength: 100));
        }
    }
}
