namespace Project300_BioStudent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfilePhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfilePhoto", c => c.Binary(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfilePhoto");
        }
    }
}
