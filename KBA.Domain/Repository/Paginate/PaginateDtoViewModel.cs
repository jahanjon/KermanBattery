
namespace KBA.Domain.Repository.Paginate
{
    public class PaginateDtoViewModel
    {
        public int TotalPages { get; set; }
        public int LastPage { get; set; }
        public int PageCount { get; set; }
        public long TotalRecords { get; set; }
        public int RowPages { get; set; }
        public int PageId { get; set; }
        public int Step { get; set; }
        public string ControllerName { get; set; }
        public string Action { get; set; }
    }
}
