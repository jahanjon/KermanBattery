using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Contract.Auth
{
    public interface ISellerLoginService
    {
        Task RecordLoginAsync(string phoneNumber, string ipAddress);
        Task RecordLogoutAsync(string phoneNumber);
    }
}
