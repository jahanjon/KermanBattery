

namespace KBA.Domain.Repository.Paginate
{
    public class RepositoryPaginateResult<T>
    {
        public List<T> Items { get; set; }
        public RepositoryPaginateResultItems PagedResultIndex { get; set; }
    }
}
