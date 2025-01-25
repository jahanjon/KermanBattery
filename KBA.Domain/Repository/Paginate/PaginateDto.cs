using KBA.Farmework.Domain;

namespace KBA.Domain.Repository.Paginate
{
    public class PaginateDto<T> where T : BaseEntity
    {
        public IEnumerable<T> Records { get; set; }
        public PaginateDtoViewModel PaginateDtoViewModel { get; set; }
    }
}
