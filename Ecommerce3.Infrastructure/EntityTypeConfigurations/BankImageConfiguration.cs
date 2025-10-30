using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class BankImageConfiguration : IEntityTypeConfiguration<BankImage>
{
    public void Configure(EntityTypeBuilder<BankImage> builder)
    {
        //Properties.
        builder.Property(x => x.BankId).HasColumnType("integer").HasColumnOrder(18);

        //Relations.
        builder.HasOne(x => x.Bank)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.BankId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}