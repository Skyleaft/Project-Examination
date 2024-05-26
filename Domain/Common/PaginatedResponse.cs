using Microsoft.EntityFrameworkCore;

namespace Domain.Common;
public class PaginatedResponse<T>
{
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int TotalItems { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPage;
    public List<T> Items { get; init; } = new List<T>();

    public PaginatedResponse(List<T> items,int page, int count,int pageSize)
    {
        TotalItems = count;
        CurrentPage = page;
        TotalPage = (int)Math.Ceiling(count / (double)pageSize);
        Items.AddRange(items);
    }
    public PaginatedResponse() { }
}

public static class PaginatedListHelper
{

    public const int DefaultPageSize = 15;
    public const int DefaultCurrentPage = 1;

    public static async Task<PaginatedResponse<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int currentPage, int pageSize)
    {
        currentPage = currentPage > 0 ? currentPage : DefaultCurrentPage;
        pageSize = pageSize > 0 ? pageSize : DefaultPageSize;
        var count = await source.CountAsync();
        var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedResponse<T>(items, count, currentPage, pageSize);
    }
}
