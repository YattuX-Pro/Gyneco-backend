
namespace Gyneco.Application.DTOs.Search
{
    public class SearchDTO
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public Dictionary<string, string> Filters { get; set; }
    }
}
