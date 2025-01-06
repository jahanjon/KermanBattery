using AutoMapper;
using KermanBattery.Farmework.Domain;
using Microsoft.EntityFrameworkCore;
using RepresentativePanel.Application.Contract.Seller;
using RepresentativePanel.Application.Dto;
using RepresentativePanel.DataAccess.Repository;
using RepresentativePanel.Domain.Core;
using RepresentativePanel.Domain.Entity.SellerAgg;
using RepresentativePanel.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Service
{
    public class SellerService : ISellerService
    {
        private readonly IGenericRepository<Seller> sellerRepsoiotry;
        private readonly IMapper mapper;

        public SellerService(IGenericRepository<Seller> sellerRepsoiotry, IMapper mapper)
        {
            this.sellerRepsoiotry = sellerRepsoiotry;
            this.mapper = mapper;
        }

        public async Task<Result<DashboardDto>> GetSellerData(string phoneNumber)
        {

            var seller = await sellerRepsoiotry.FindAsNoTracking(s => s.PhoneNumber == phoneNumber);

            if (seller == null)
            {
                return Result<DashboardDto>.Failure(-400, "BadRequest");
            }


            var dashboardDto = mapper.Map<DashboardDto>(seller);
            return Result<DashboardDto>.Success(200, "Data retrieved successfully", dashboardDto);
        }


        public async Task<Result<bool>> UpdateAndInsertProfile(DashboardDto dashboardDto)
        {
            try
            {

                var seller = await sellerRepsoiotry.Find(x => x.PhoneNumber == dashboardDto.PhoneNumber);

                if (seller == null)
                {
                    return Result<bool>.Failure(-400, "نماینده با این شما تلفن وجود ندارد");
                }


                seller.UpdateProfile(dashboardDto.Title, dashboardDto.Province, dashboardDto.Address);


                var updateResult = await sellerRepsoiotry.Update(seller);

                if (!updateResult)
                {
                    return Result<bool>.Failure(-500, "Failed to update seller profile");
                }

                return Result<bool>.Success(200, "Profile updated successfully", true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(-500, $"An error occurred: {ex.Message}");
            }
        }


        public async Task<Result<DashboardDto>> UpdateProfileAsync(string phoneNumber)
        {
            var seller = await sellerRepsoiotry.Find(x => x.PhoneNumber == phoneNumber);

            if (seller == null)
            {
                return Result<DashboardDto>.Failure(-400, "Seller not found");
            }

            var dashboardDto = mapper.Map<DashboardDto>(seller);
            return Result<DashboardDto>.Success(200, "Profile data retrieved", dashboardDto);
        }

    }
}
