namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class candidateIdAdded : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.CandidateDTOes");
            //AddColumn("dbo.CandidateDTOes", "CandidateId", c => c.String(nullable: false, maxLength: 128));
            //AlterColumn("dbo.CandidateDTOes", "FirstName", c => c.String(nullable: false));
            //AddPrimaryKey("dbo.CandidateDTOes", "CandidateId");
        }
        
        public override void Down()
        {
            //DropPrimaryKey("dbo.CandidateDTOes");
            //AlterColumn("dbo.CandidateDTOes", "FirstName", c => c.String(nullable: false, maxLength: 128));
            //DropColumn("dbo.CandidateDTOes", "CandidateId");
            //AddPrimaryKey("dbo.CandidateDTOes", "FirstName");
        }
    }
}
