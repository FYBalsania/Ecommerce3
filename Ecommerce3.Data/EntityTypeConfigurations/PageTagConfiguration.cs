using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Data.EntityTypeConfigurations;

public class PageTagConfiguration : IEntityTypeConfiguration<PageTag>
{
    public void Configure(EntityTypeBuilder<PageTag> builder)
    {
    }
}