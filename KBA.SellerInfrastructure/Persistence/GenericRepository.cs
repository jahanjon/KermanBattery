using KBA.Farmework.Domain;
using Microsoft.EntityFrameworkCore;
using KBA.Domain.Repository;
using System.Linq.Expressions;


namespace KBA.SellerInfrastructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly KermanBatterySellerContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public GenericRepository(KermanBatterySellerContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }



        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsNoTracking()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> Paginate(int currentPage, int pageSize = 10)
        {
            return await entities.OrderByDescending(x => x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> Get(long id)
        {
            //context.ChangeTracker.Clear();
            return await context.Set<T>().SingleOrDefaultAsync(e => e.Id == id); ;
        }

        public async Task<T> GetAsNoTracking(int id)
        {
            return await context.Set<T>().AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }


        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }


        public async Task<T> FindAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            //context.ChangeTracker.Clear();
            return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }


        public List<T> FindInList(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where<T>(predicate).ToList();
        }


        public List<T> FindInListAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where<T>(predicate).AsNoTracking().OrderByDescending(x => x.Id).ToList(); ;
        }


        public async Task<List<T>> FindInListAsNoTrackingAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where<T>(predicate).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }





        public async Task<T> LastOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> orderBy)
        {
            return await context.Set<T>().AsNoTracking().OrderByDescending(orderBy).LastOrDefaultAsync(predicate);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> orderBy)
        {
            return await context.Set<T>().AsNoTracking().OrderByDescending(orderBy).FirstOrDefaultAsync(predicate);
        }



        public async Task<List<T>> TakeTopAsNoTrackingWithWhere(int takeCount, Expression<Func<T, DateTime>> orderBy)
        {
            return await context.Set<T>().AsNoTracking().OrderByDescending(orderBy).Take(takeCount).ToListAsync();
        }

        public List<T> TakeTopAsNoTracking(int takeCount)
        {
            return context.Set<T>().AsNoTracking().OrderByDescending(p => p.Id).Take(takeCount).ToList();
        }


        public Task<long> Sum(Expression<Func<T, bool>> predicateWhere, Expression<Func<T, long>> predicate)
        {
            return context.Set<T>().Where(predicateWhere).SumAsync(predicate);
        }



        public long GetMax(Func<T, long> columnSelector)
        {
            return context.Set<T>().Max(columnSelector);
        }




        public long GetMaxWhere(Func<T, long> columnSelector, Expression<Func<T, bool>> predicate)
        {
            var res = context.Set<T>().Where(predicate);

            return (res.Count() > 0) ? res.Max(columnSelector) : 0;
        }


        public List<T> PaginateWhere(Expression<Func<T, bool>> predicate, int currentPage, int pageSize = 10)
        {
            return context.Set<T>().Where<T>(predicate).OrderByDescending(x => x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }




        public async Task<bool> Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                var sss = await entities.AddAsync(entity);
                return await context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception e)
            {
                using (StreamWriter writer = new StreamWriter("InsertException.txt", append: true))
                {
                    writer.WriteLine(DateTime.Now + " " + typeof(T) + e.Message + e.InnerException);
                }
                return false;
            }
        }

        public async Task<long> InsertReturnId(T entity)
        {
            try
            {
                if (entity == null)
                {
                    return 0;
                }
                await entities.AddAsync(entity);
                await context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception e)
            {

                using (StreamWriter writer = new StreamWriter("InsertReturnIdException.txt", append: true))
                {
                    writer.WriteLine(DateTime.Now + " " + typeof(T));
                }

                return 0;
            }
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                context.Entry(entity).State = EntityState.Modified;


                return await context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception e)
            {

                using (StreamWriter writer = new StreamWriter("UpdateException.txt", append: true))
                {
                    writer.WriteLine(DateTime.Now + " " + typeof(T));
                }

                return false;
            }

        }




        //public async Task<List<T>> BulkUpdate(List<T> entity)
        //{
        //    try
        //    {
        //        if (entity == null)
        //        {
        //            return new List<T>();
        //        }

        //        await context.BulkUpdateAsync(entity);
        //        return entity;
        //    }
        //    catch (Exception e)
        //    {
        //        var sss = e.Message;
        //        return new List<T>();
        //    }

        //}



        //public async Task<List<T>> BulkInsert(List<T> entity)
        //{
        //    try
        //    {
        //        if (entity == null)
        //        {
        //            return new List<T>();
        //        }

        //        BulkConfig bulkCOnfig = new BulkConfig()
        //        {
        //            SetOutputIdentity = true
        //        };

        //        await context.BulkInsertAsync(entity, bulkCOnfig);
        //        return entity;
        //    }
        //    catch (Exception e)
        //    {
        //        var sss = e.Message;
        //        return new List<T>();
        //    }

        //}





        public async Task<bool> DeleteByWhere(string column, long id)
        {
            var command = $"DELETE FROM Tbl_Permission WHERE {column} = {id}";


            var res = await context.Database
                 .ExecuteSqlRawAsync(command);
            return true;
        }


        public async Task<bool> Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }
                entities.Remove(entity);
                return await context.SaveChangesAsync() > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }


        public int TotalPages(int pageSize)
        {
            try
            {

                if (GetCount() > 0)
                {
                    var dec = Convert.ToDecimal(GetCount());

                    return (int)Math.Ceiling(decimal.Divide(dec, pageSize));
                }
                else
                {
                    return 0;
                }


            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int TotalPages(Expression<Func<T, bool>> predicate, int pageSize)
        {
            try
            {
                long getCount = GetCountWhere(predicate);
                if (getCount > 0)
                {
                    var dec = Convert.ToDecimal(getCount);

                    return (int)Math.Ceiling(decimal.Divide(dec, pageSize));
                }
                else
                {
                    return 0;
                }


            }
            catch (Exception)
            {
                return 0;
            }
        }


        public long GetCount()
        {
            return entities.Count();
        }


        public long GetCountWhere(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where<T>(predicate).Count();
        }


        public async Task<bool> IsExist(long id)
        {
            return await context.Set<T>().AnyAsync<T>(x => x.Id == id);
        }




        public List<T> ListPaginate(List<T> list, int currentPage, int pageSize = 10)
        {
            var res = list.OrderByDescending(x => x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return res;

        }



        public async Task<T> GenericInnerJoinFirstOrDefault(List<string> include, Expression<Func<T, bool>> predicateWhere)
        {
            var query = context.Set<T>().AsSingleQuery();

            foreach (var item in include)
            {
                query = query.Include(item);
            }
            return await query.Where(predicateWhere).FirstOrDefaultAsync();
        }



        public async Task<IEnumerable<T>> GenericInnerJoinWithOneTable(string include, Expression<Func<T, bool>> predicateWhere)
        {
            return await context.Set<T>().Where(predicateWhere).Include(include).ToListAsync();
        }

        public async Task<List<T>> GenericMultipleInnerJoin(List<string> include, Expression<Func<T, bool>> predicateWhere)
        {
            var query = context.Set<T>().Where(predicateWhere).AsQueryable();
            foreach (var item in include)
            {
                query = query.Include(item);
            }

            return await query.ToListAsync();
        }

        public Task<List<T>> BulkInsert(List<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> BulkUpdate(List<T> entity)
        {
            throw new NotImplementedException();
        }

        //public async Task<PaginateDto<T>> PaginateRecords(string controllerName, string actionName, string rowPages, int pageId)
        //{
        //    var paginateData = new PaginateDto<T>();

        //    paginateData.PaginateDtoViewModel = new();

        //    var rowPage = Convert.ToInt32(rowPages);
        //    paginateData.PaginateDtoViewModel.TotalPages = TotalPages(rowPage);
        //    paginateData.PaginateDtoViewModel.LastPage = paginateData.PaginateDtoViewModel.TotalPages;
        //    paginateData.PaginateDtoViewModel.PageCount = paginateData.PaginateDtoViewModel.TotalPages;
        //    paginateData.PaginateDtoViewModel.TotalRecords = GetCount();
        //    paginateData.PaginateDtoViewModel.RowPages = rowPage;
        //    paginateData.PaginateDtoViewModel.ControllerName = controllerName;
        //    paginateData.PaginateDtoViewModel.Action = actionName;
        //    paginateData.PaginateDtoViewModel.PageId = pageId; ;
        //    paginateData.PaginateDtoViewModel.Step = 3;
        //    paginateData.Records = await Paginate(paginateData.PaginateDtoViewModel.PageId, paginateData.PaginateDtoViewModel.RowPages);
        //    return paginateData;
    }
}




