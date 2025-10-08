namespace Ecommerce3.Contracts.Paging;

public sealed record PagedList<T>
{
    public required List<T> Items { get; init; }
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public required int TotalCount { get; init; }
    
    public bool HasNextPage => PageNumber * PageSize < TotalCount;
    
    public bool HasPreviousPage => PageNumber > 1;
}