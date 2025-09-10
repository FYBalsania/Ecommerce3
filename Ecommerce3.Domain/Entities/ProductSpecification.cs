using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductSpecification : Entity
{
    public int? ProductSpecificationGroupId { get; private set; }
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Breadcrumb { get; private set; }
    public DataType DataType { get; private set; }
    public int SortOrder { get; private set; }
}