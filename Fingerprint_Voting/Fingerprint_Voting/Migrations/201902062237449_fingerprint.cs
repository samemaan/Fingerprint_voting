namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fingerprint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserFingerprint", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserFingerprint");
        }
    }
}
