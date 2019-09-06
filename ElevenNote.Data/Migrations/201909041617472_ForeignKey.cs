namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Note", "CategoryID");
            AddForeignKey("dbo.Note", "CategoryID", "dbo.Category", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "CategoryID", "dbo.Category");
            DropIndex("dbo.Note", new[] { "CategoryID" });
        }
    }
}
