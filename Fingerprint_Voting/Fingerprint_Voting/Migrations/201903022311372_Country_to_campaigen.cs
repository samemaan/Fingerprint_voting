namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Country_to_campaigen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetCampaign", "Country", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetCampaign", "Country");
        }
    }
}
