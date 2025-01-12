using AutoMapper;
using KBA.SellerApplication.Contract.Seller;
using KBA.Domain.Entity.SellerAgg;

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
