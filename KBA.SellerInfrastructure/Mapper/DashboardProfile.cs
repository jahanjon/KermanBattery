using AutoMapper;
using KBA.Domain.Entity.SellerAgg;
using KBA.SellerApplication.Contract.Seller;

namespace KBA.SellerInfrastructure.Mapper
{
    public class DashbordProfile : Profile
    {
        public DashbordProfile()
        {
            CreateMap<Seller, DashboardDto>().ReverseMap();

        }
    }
}
