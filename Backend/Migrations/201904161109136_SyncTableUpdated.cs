namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncTableUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Syncs", "GroupId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Syncs", "GroupId");
        }
    }
}
