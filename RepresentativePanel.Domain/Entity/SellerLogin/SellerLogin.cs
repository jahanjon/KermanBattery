using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepresentativePanel.Domain.Entity.SellerAgg;

namespace RepresentativePanel.Domain.Entity.SellerLogin
{
    public class SellerLogin
    {
        public Guid Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime LoginTime { get; private set; }
        public DateTime? LogoutTime { get; private set; }
        public string IpAddress { get; private set; }
        public int SellerId { get; private set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        #region Relation

        public Seller Seller { get; private set; }

        #endregion

        #region Constructor

        public SellerLogin(string phoneNumber, string ipAddress, int sellerId)
        {
            Id = Guid.NewGuid();
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            LoginTime = DateTime.UtcNow;
            IpAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
            SellerId = sellerId;
        }

        public void SetLogoutTime()
        {
            if (LogoutTime != null)
                throw new InvalidOperationException("Logout time is already set.");
            LogoutTime = DateTime.UtcNow;
        }
        #endregion
    }
}
