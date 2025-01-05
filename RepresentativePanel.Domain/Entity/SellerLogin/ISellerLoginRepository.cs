using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Domain.Entity.SellerLogin
{
    public interface ISellerLoginRepository 
    {
        Task<SellerLogin> GetActiveLoginAsync(string phoneNumber);
    }
}
