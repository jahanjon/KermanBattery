using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Auth.Dto
{
    public class TokenResultDto
    {
        public string Token { get; set; }
        public ResultCode MessageCode { get; set; }
    }
}
