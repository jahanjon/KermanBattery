using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Contract.Seller.Auth
{
    public class ChangePasswordDto
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public string NewPassword { get; set; }
    }
}
