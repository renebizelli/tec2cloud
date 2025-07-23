using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.Title).IsRequired().HasMaxLength(128);
        builder.Property(u => u.Description).IsRequired().HasColumnType(SqlDbType.Text.ToString());
        builder.Property(u => u.Category).IsRequired().HasMaxLength(64);
        builder.Property(u => u.Price).IsRequired();
        builder.Property(u => u.Image).IsRequired().HasMaxLength(128);
    }
}
