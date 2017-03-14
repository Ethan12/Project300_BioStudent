namespace Project300_BioStudent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentAttendance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        Attended = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .ForeignKey("dbo.StudentUserAccount", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ModuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentAttendance", "StudentId", "dbo.StudentUserAccount");
            DropForeignKey("dbo.StudentAttendance", "ModuleId", "dbo.Modules");
            DropIndex("dbo.StudentAttendance", new[] { "ModuleId" });
            DropIndex("dbo.StudentAttendance", new[] { "StudentId" });
            DropTable("dbo.StudentAttendance");
        }
    }
}
