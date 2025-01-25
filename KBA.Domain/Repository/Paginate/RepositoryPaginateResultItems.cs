

namespace KBA.Domain.Repository.Paginate
{
    public class RepositoryPaginateResultItems
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageStep { get; set; }
        public int TotalPages { get; set; }
        public string ActionName { get; set; }
    }
}
