namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Note", "ModifiedUtc");
        }
    }
}
