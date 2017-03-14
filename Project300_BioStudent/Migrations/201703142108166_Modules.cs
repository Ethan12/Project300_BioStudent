namespace Project300_BioStudent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        LecturerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LecturerId)
                .Index(t => t.LecturerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "LecturerId", "dbo.AspNetUsers");
            DropIndex("dbo.Modules", new[] { "LecturerId" });
            DropTable("dbo.Modules");
        }
    }
}
