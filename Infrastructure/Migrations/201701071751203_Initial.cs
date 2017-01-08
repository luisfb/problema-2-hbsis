using Infrastructure.UnitOfWork;

namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            //try
            //{
            //    Down();
            //}
            //catch (Exception)
            //{}

            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 100, unicode: false),
                        code = c.String(maxLength: 50, unicode: false),
                        author = c.String(nullable: false, maxLength: 255, unicode: false),
                        publisher = c.String(maxLength: 255, unicode: false),
                        location = c.String(maxLength: 255, unicode: false),
                        quantity = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        insertdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Book");
        }
    }
}
