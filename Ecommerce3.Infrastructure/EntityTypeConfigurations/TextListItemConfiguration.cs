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
        builder.HasDiscriminator<string>("Discriminator")
            .HasValue<ProductTextListItem>(nameof(ProductTextListItem));
        
        //Filters
        builder.HasQueryFilter(x => x.DeletedAt == null);
        
        //Properties.
        builder.Property("Discriminator").HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(2);
        builder.Property(x => x.Type).HasConversion<string>().HasColumnOrder(3);
        builder.Property(x => x.Text).HasColumnType("citext").HasColumnOrder(4);
        builder.Property(x => x.SortOrder).HasColumnType("decimal(18,2)").HasColumnOrder(5);
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
        builder.HasIndex(x => x.Type).HasDatabaseName($"IX_{nameof(TextListItem)}_{nameof(TextListItem.Type)}");
        builder.HasIndex(x => x.Text).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(TextListItem)}_{nameof(TextListItem.Text)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(TextListItem)}_{nameof(TextListItem.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(TextListItem)}_{nameof(TextListItem.DeletedAt)}");
        
        //relations.
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