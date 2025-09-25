using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class KVPListItemConfiguration : IEntityTypeConfiguration<KVPListItem>
{
    public void Configure(EntityTypeBuilder<KVPListItem> builder)
    {
        //Table.
        builder.ToTable(nameof(KVPListItem));

        //Key.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Discriminator.
        builder.HasDiscriminator(x => x.Discriminator)
            .HasValue<CategoryKVPListItem>(nameof(CategoryKVPListItem))
            .HasValue<ProductKVPListItem>(nameof(ProductKVPListItem));

        //Properties.
        builder.Property(x => x.Discriminator).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(2);
        builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(16).HasColumnType("varchar(16)")
            .HasColumnOrder(3);
        builder.Property(x => x.Key).HasColumnType("text").HasColumnOrder(4);
        builder.Property(x => x.Value).HasColumnType("text").HasColumnOrder(5);
        builder.Property(x => x.SortOrder).HasColumnType("int").HasColumnOrder(6);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);
        
        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Indexes.
        builder.HasIndex(x => x.Discriminator).HasDatabaseName($"IX_{nameof(KVPListItem)}_{nameof(KVPListItem.Discriminator)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(KVPListItem)}_{nameof(KVPListItem.DeletedAt)}");

        //Relations.
        builder.HasOne(x => (AppUser?)x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.UpdatedByUser)
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.DeletedByUser)
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}