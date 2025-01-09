using RepresentativePanel.Application.Dto;
using RepresentativePanel.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Contract.Seller
{
    public interface ISellerService
    {
        Task<Result<DashboardDto>> GetSellerData(int sellerId);
        Task<Result<DashboardDto>> UpdateProfileAsync(int sellerId);
        Task<Result<bool>> UpdateAndInsertProfile(DashboardDto dashboard);
    }
}
