using AutoMapper;
using KermanBatterySeller.Application.Contract.Auth.Dto;
using RepresentativePanel.Domain.Entity.SellerLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KermanBatterySeller.Infrastructure.Mapper
{
    public class SellerLoginProfile : Profile
    {
        public SellerLoginProfile()
        {
            CreateMap<SellerLogin, SellerLoginDto>().ReverseMap();
        }
    }
}
