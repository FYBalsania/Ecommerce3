using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class TextListItemConfiguration : IEntityTypeConfiguration<TextListItem>
{
    public void Configure(EntityTypeBuilder<TextListItem> builder)
    {
        //Table.
        builder.ToTable(nameof(TextListItem));
        
        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);
        
        //discriminator.
        builder.HasDiscriminator(x => x.Type)
            .HasValue<ProductTextListItem>(nameof(ProductTextListItem));
        
        //Properties.
        builder.Property(x => x.Type).HasMaxLength(32).HasColumnType("varchar(32)").HasColumnOrder(2);
        builder.Property(x => x.TextListItemType).HasConversion<string>().HasColumnOrder(3);
        builder.Property(x => x.Text).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(4);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);
        
        //Indexes.
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(TextListItem)}_{nameof(TextListItem.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(TextListItem)}_{nameof(TextListItem.DeletedAt)}");
        
        //relations.
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}