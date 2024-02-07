namespace e_Estoque_API.Core.Models;

public class PagedResult
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }

    public int FirstRowOnPage
    {
        get => (CurrentPage - 1) * PageSize + 1;
    }

    public int LastRowOnPage
    {
        get => Math.Min(CurrentPage * PageSize, RowCount);
    }
}