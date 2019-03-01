namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VotingData1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VotesDTOes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CandidateId = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VotesDTOes");
        }
    }
}
