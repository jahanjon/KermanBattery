using KBA.Framework.Core;
using KBA.SellerApplication.Contract.CosFormat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.CosFormat
{
    public interface ICosFormatService
    {
        Task<ApplicationPaginateResult<CosFormatViewModel>> CosFormatPaginate(int pageIndex, int pageSize, string actionName);
    }
}
