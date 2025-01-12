using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Auth.Dto
{
    public enum ResultCode
    {
        Success = 1,
        DuplicateNationalNumber = 2,
        DuplicatePhoneNumber = 3,
        BadRequest = 4,
        IncorrectEmailOrPassword = 5,
    }
}
