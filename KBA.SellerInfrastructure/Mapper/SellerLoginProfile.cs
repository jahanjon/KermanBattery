using AutoMapper;
using KBA.SellerApplication.Contract.Auth.Dto;
using KBA.Domain.Entity.SellerLogin;

namespace KBA.SellerInfrastructure.Mapper
{
    public class SellerLoginProfile : Profile
    {
        public SellerLoginProfile()
        {
            CreateMap<SellerLogin, SellerLoginDto>().ReverseMap();
        }
    }
}
