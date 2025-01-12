using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Auth.Dto
{
    public class SellerLoginDto
    {
        public string PhoneNumber { get; private set; }
        public DateTime LoginTime { get; private set; }
        public DateTime? LogoutTime { get; private set; }
        public string IpAddress { get; private set; }
        public int SellerId { get; private set; }

    }
}
