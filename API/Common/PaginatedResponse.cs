using Microsoft.EntityFrameworkCore;

namespace API.Common;
public class PaginatedResponse<T>
{
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int Count { get; set; }
    public bool HasNext { get; set; }
    public List<T> Items { get; set; }

    public PaginatedResponse(List<T> datas,int page, int count,int pageSize)
    {
        CurrentPage = page;
        TotalPage = (int)Math.Ceiling(count / (double)pageSize);
        Items.AddRange(datas);
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
