using AutoMapper;
using KBA.SellerApplication.Contract.Auth.Dto;
using KBA.Domain.Entity.SellerLogin;

namespace KBA.SellerInfrastructure.Mapper
{
    public class SellerLoginReportProfile : Profile
    {
        public SellerLoginReportProfile()
        {
            CreateMap<SellerLoginReport, SellerLoginReportDto>().ReverseMap();
        }
    }
}
