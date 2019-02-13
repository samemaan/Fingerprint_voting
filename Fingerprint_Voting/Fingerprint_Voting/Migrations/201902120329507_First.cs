namespace Fingerprint_Voting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.TestDTOes",
            //    c => new
            //        {
            //            UserName = c.String(nullable: false, maxLength: 128),
            //            Surname = c.String(),
            //        })
            //    .PrimaryKey(t => t.UserName);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.TestDTOes");
        }
    }
}
