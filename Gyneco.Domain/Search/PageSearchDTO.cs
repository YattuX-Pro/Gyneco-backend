namespace Gyneco.Application.Models.Search;

public class PageSearchDTO
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public Dictionary<string, string> Filters { get; set; }

}