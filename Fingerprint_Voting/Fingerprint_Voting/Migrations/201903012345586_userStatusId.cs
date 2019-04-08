namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userStatusId : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.AspNetUsers", "UserStatusId", c => c.String());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.AspNetUsers", "UserStatusId");
        }
    }
}
