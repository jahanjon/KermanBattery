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
        Task<Result<DashboardDto>> GetSellerData(string phoneNumber);
        Task<Result<DashboardDto>> UpdateProfileAsync(string phoneNumber);
        Task<Result<bool>> UpdateAndInsertProfile(DashboardDto dashboard);
    }
}
