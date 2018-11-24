namespace RESTAURANT.API.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestaurentDBSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Note = c.String(),
                        CreatedBy = c.String(),
                        Created = c.DateTime(),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Except = c.String(),
                        Utility = c.String(),
                        Title = c.String(maxLength: 255),
                        Note = c.String(),
                        CreatedBy = c.String(),
                        Created = c.DateTime(),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(),
                        Category_ID = c.Int(),
                        Kind_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_ID)
                .ForeignKey("dbo.Kinds", t => t.Kind_ID)
                .Index(t => t.Category_ID)
                .Index(t => t.Kind_ID);
            
            CreateTable(
                "dbo.Kinds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Note = c.String(),
                        CreatedBy = c.String(),
                        Created = c.DateTime(),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Excepts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Note = c.String(),
                        CreatedBy = c.String(),
                        Created = c.DateTime(),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Note = c.String(),
                        CreatedBy = c.String(),
                        Created = c.DateTime(),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(),
                        Status_ID = c.Int(),
                        Table_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Status", t => t.Status_ID)
                .ForeignKey("dbo.Tables", t => t.Table_ID)
                .Index(t => t.Status_ID)
                .Index(t => t.Table_ID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Note = c.String(),
                        CreatedBy = c.String(),
                        Created = c.DateTime(),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Note = c.String(),
                        CreatedBy = c.String(),
                        Created = c.DateTime(),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Utilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Note = c.String(),
                        CreatedBy = c.String(),
                        Created = c.DateTime(),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Table_ID", "dbo.Tables");
            DropForeignKey("dbo.Orders", "Status_ID", "dbo.Status");
            DropForeignKey("dbo.Details", "Kind_ID", "dbo.Kinds");
            DropForeignKey("dbo.Details", "Category_ID", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "Table_ID" });
            DropIndex("dbo.Orders", new[] { "Status_ID" });
            DropIndex("dbo.Details", new[] { "Kind_ID" });
            DropIndex("dbo.Details", new[] { "Category_ID" });
            DropTable("dbo.Utilities");
            DropTable("dbo.Tables");
            DropTable("dbo.Status");
            DropTable("dbo.Orders");
            DropTable("dbo.Excepts");
            DropTable("dbo.Kinds");
            DropTable("dbo.Details");
            DropTable("dbo.Categories");
        }
    }
}
