using AutoMapper;
using KBA.Domain.Entities.CosFormats;
using KBA.Domain.Repository;
using KBA.Framework.Core;
using KBA.SellerApplication.Contract.CosFormat;
using KBA.SellerApplication.Contract.CosFormat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Service
{
    public class CosFormatService : ICosFormatService
    {

        private readonly IGenericRepository<CosFormat> cosFormatRepositroy;
        private readonly IMapper mapper;
        public CosFormatService(IGenericRepository<CosFormat> cosFormatRepositroy, IMapper mapper)
        {
            this.cosFormatRepositroy = cosFormatRepositroy;
            this.mapper = mapper;
        }


        public async Task<ApplicationPaginateResult<CosFormatViewModel>> CosFormatPaginate(int pageIndex, int pageSize, string actionName)
        {
            var pagedResult = await cosFormatRepositroy.GetPagedAsync(pageIndex, pageSize, actionName);

            return new ApplicationPaginateResult<CosFormatViewModel>
            {
                ApplicationPaginateResultItems = mapper.Map<ApplicationPaginateResultItems>(pagedResult.PagedResultIndex),
                Items = mapper.Map<List<CosFormatViewModel>>(pagedResult.Items)
            };
        }
    }
}
