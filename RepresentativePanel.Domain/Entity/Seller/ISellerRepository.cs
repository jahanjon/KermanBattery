using RepresentativePanel.Application.Dto;
using RepresentativePanel.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Domain.Entity.SellerAgg
{
    public interface ISellerRepository
    {
        bool VerifyHashedPassword(string hashedPassword, string password);
        string HashPassword(string password);
        
    }
}
