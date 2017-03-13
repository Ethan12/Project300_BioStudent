namespace Project300_BioStudent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentGrades : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentGrades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        Grade = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentUserAccount", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentGrades", "StudentId", "dbo.StudentUserAccount");
            DropIndex("dbo.StudentGrades", new[] { "StudentId" });
            DropTable("dbo.StudentGrades");
        }
    }
}
