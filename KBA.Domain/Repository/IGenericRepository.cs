
using KBA.Farmework.Domain;
using KBA.Domain.Repository.Paginate;
using System.Linq.Expressions;


namespace KBA.Domain.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<RepositoryPaginateResult<T>> GetPagedByWhereAndTwoInclude(int pageIndex, int pageSize, string actionName, Expression<Func<T, bool>> predicate, string firstInclude, string secondInclude);
        Task<RepositoryPaginateResult<T>> GetPagedAsync(int pageIndex, int pageSize, string actionName);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllAsNoTracking();
        Task<IEnumerable<T>> Paginate(int currentPage, int pageSize);

        Task<T> GetAsNoTracking(long id);
        Task<T> Get(long id);
        Task<bool> Insert(T entity);
        Task<long> InsertReturnId(T entity);

        Task<List<T>> BulkInsert(List<T> entity);
        Task<bool> Update(T entity);
        Task<List<T>> BulkUpdate(List<T> entity);
        Task<bool> Delete(T entity);
        Task<bool> DeleteByWhere(string column, long id);

        Task<bool> IsExist(long id);
        int TotalPages(int pageSize);
        int TotalPages(Expression<Func<T, bool>> predicate, int pageSize);
        long GetCount();
        //long MaxProductionCounter();
        long GetCountWhere(Expression<Func<T, bool>> predicate);
        List<T> FindInList(Expression<Func<T, bool>> predicate);
        List<T> FindInListAsNoTracking(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindInListAsNoTrackingAsync(Expression<Func<T, bool>> predicate);
        List<T> PaginateWhere(Expression<Func<T, bool>> predicate, int currentPage, int pageSize);
        List<T> TakeTopAsNoTracking(int takeCount);
        Task<T> FindAsNoTracking(Expression<Func<T, bool>> predicate);
        Task<T> Find(Expression<Func<T, bool>> predicate);

        Task<long> Sum(Expression<Func<T, bool>> predicateWhere, Expression<Func<T, long>> predicate);

        Task<T> LastOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> orderBy);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> orderBy);

        List<T> ListPaginate(List<T> list, int currentPage, int pageSize);

        Task<IEnumerable<T>> GenericInnerJoinWithOneTable(string include, Expression<Func<T, bool>> predicateWhere);

        Task<List<T>> GenericMultipleInnerJoin(List<string> include, Expression<Func<T, bool>> predicateWhere);
        Task<List<T>> TakeTopAsNoTrackingWithWhere(int takeCount, Expression<Func<T, DateTime>> orderBy);

        long GetMax(Func<T, long> columnSelector);


        Task<PaginateDto<T>> PaginateRecords(string controllerName, string actionName, string rowPages, int pageId);

        long GetMaxWhere(Func<T, long> columnSelector, Expression<Func<T, bool>> predicate);



    }

}
