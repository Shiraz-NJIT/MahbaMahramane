namespace MahbaExtention.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PN = c.String(maxLength: 128),
                        FileName = c.String(),
                        AttachedToDossier = c.Boolean(nullable: false),
                        ParentDocumentID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dossiers", t => t.PN)
                .Index(t => t.PN);
            
            CreateTable(
                "dbo.Dossiers",
                c => new
                    {
                        PN = c.String(nullable: false, maxLength: 128),
                        NN = c.String(),
                        Name = c.String(),
                        Family = c.String(),
                        FatherName = c.String(),
                        Reshte = c.String(),
                        Maghta = c.String(),
                    })
                .PrimaryKey(t => t.PN);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "PN", "dbo.Dossiers");
            DropIndex("dbo.Documents", new[] { "PN" });
            DropTable("dbo.Dossiers");
            DropTable("dbo.Documents");
        }
    }
}
