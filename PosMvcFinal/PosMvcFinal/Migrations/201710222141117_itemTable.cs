namespace PosMvcFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Image", c => c.Binary());
        }
    }
}
