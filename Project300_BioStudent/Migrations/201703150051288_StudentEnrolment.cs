namespace Project300_BioStudent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentEnrolment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentEnrolment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .ForeignKey("dbo.StudentUserAccount", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.ModuleId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentEnrolment", "StudentId", "dbo.StudentUserAccount");
            DropForeignKey("dbo.StudentEnrolment", "ModuleId", "dbo.Modules");
            DropIndex("dbo.StudentEnrolment", new[] { "StudentId" });
            DropIndex("dbo.StudentEnrolment", new[] { "ModuleId" });
            DropTable("dbo.StudentEnrolment");
        }
    }
}
