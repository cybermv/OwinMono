namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fruits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Color = c.Int(nullable: false),
                        Grocer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groceries", t => t.Grocer_Id)
                .Index(t => t.Grocer_Id);
            
            CreateTable(
                "dbo.Groceries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fruits", "Grocer_Id", "dbo.Groceries");
            DropIndex("dbo.Fruits", new[] { "Grocer_Id" });
            DropTable("dbo.Groceries");
            DropTable("dbo.Fruits");
        }
    }
}
