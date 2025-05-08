namespace Contracts.Pagination;

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    
    public PaginatedResult(IEnumerable<T> items, int totalItems, int currentPage, int pageSize)
    {
        Items = items;
        TotalItems = totalItems;
        CurrentPage = currentPage;
        TotalPages = CalculateTotalPages(pageSize);
    }
    
    private int CalculateTotalPages(int pageSize)
    {
        if (pageSize <= 0) 
            throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));
        
        return (int)Math.Ceiling((double)TotalItems / pageSize);
    }
}