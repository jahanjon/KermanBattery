using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.Domain.Entity.SellerLogin
{
    public interface ISellerLoginReportRepository 
    {
        Task<SellerLoginReport> GetActiveLoginAsync(int sellerId);
    }
}
