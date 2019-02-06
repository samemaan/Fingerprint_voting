namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UserFingerprint");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserFingerprint", c => c.Binary(nullable: false));
        }
    }
}
