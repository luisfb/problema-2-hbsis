
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Models.Entities;

namespace Infrastructure.EntityMapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Title).IsRequired().HasMaxLength(100).HasColumnName("title");
            Property(x => x.Code).IsOptional().HasMaxLength(50).HasColumnName("code");
            Property(x => x.Author).IsRequired().HasMaxLength(255).HasColumnName("author");
            Property(x => x.Publisher).IsOptional().HasMaxLength(255).HasColumnName("publisher");
            Property(x => x.Location).IsOptional().HasMaxLength(255).HasColumnName("location");
            Property(x => x.QuantityInStock).IsRequired().HasColumnName("quantity");
            Property(x => x.Price).IsRequired().HasColumnName("price");
            Property(x => x.InsertDate).IsOptional().HasColumnName("insertdate");
        }
    }
}
