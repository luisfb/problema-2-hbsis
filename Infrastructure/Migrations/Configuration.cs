using System.Collections.Generic;
using Domain.Models.Entities;

namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.UnitOfWork.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(Infrastructure.UnitOfWork.Context context)
        {
            var books = new List<Book>()
            {
                new Book("A Game of Thrones", "George R. R. Martin", 10, 49.90M),
                new Book("A Clash of Kings", "George R. R. Martin", 15, 59.50M),
                new Book("A Storm of Swords", "George R. R. Martin", 18, 61.00M),
                new Book("A Feast for Crows", "George R. R. Martin", 21, 62.99M),
                new Book("A Dance with Dragons", "George R. R. Martin", 30, 65.89M)
            };
            context.Set<Book>().AddRange(books);
        }
    }
}
