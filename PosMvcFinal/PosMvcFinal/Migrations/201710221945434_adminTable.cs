namespace PosMvcFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "PassWord", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "Name", c => c.String());
            AlterColumn("dbo.Admins", "PassWord", c => c.String());
        }
    }
}
