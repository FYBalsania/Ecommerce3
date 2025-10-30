using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class BankPageConfiguration : IEntityTypeConfiguration<BankPage>
{
    public void Configure(EntityTypeBuilder<BankPage> builder)
    {
        // Map FK to existing column
        builder.Property(x => x.BankId).HasColumnName(nameof(BankPage.BankId));
        
        //Relations.
        builder.HasOne(x => x.Bank)
            .WithOne(x => x.Page)
            .HasForeignKey<BankPage>(x => x.BankId)
            .HasPrincipalKey<Bank>(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}