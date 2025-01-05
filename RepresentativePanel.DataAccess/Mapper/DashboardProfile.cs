using AutoMapper;
using RepresentativePanel.Application.Dto;
using RepresentativePanel.Domain.Entity.SellerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.DataAccess.Mapper
{
    public class DashbordProfile : Profile
    {
        public DashbordProfile()
        {
            CreateMap<Seller, DashboardDto>().ReverseMap();

        }
    }
}
