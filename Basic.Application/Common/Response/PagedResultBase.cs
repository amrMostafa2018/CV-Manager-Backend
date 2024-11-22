
namespace Basic.Application.Common.Response
{
    public class PagedResultBase<T>
    {
        public IReadOnlyCollection<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
    }
}
