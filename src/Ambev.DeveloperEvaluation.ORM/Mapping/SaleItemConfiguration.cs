using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.Quantity).IsRequired();
        builder.Property(u => u.Price).IsRequired();
        builder.Property(u => u.Discount).IsRequired();
        builder.Property(u => u.Status).IsRequired();
        builder.Ignore(u => u.TotalPrice);

        builder.HasOne(m => m.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId);
    }
}
