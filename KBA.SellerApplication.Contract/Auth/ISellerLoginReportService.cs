using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Auth
{
    public interface ISellerLoginReportService
    {
        Task RecordLoginAsync(int sellerId, string ipAddress);
        Task RecordLogoutAsync(int sellerId);
        Task<Result<SellerLoginReportDto>> SellerActivity(int sellerId);
    }
}
