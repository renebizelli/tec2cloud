using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.BranchId).IsRequired();
        builder.Property(u => u.Status).IsRequired();
        builder.Property(u => u.TotalAmount).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();

        builder.HasMany(m => m.Items)
            .WithOne(o => o.Sale)
            .HasForeignKey(o => o.SaleId);

        builder.HasOne(m => m.User)
            .WithMany(o => o.Sales)
            .HasForeignKey(o => o.UserId);
    }
}
