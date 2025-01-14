using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBA.Farmework.Domain;
using KBA.Domain.Entity.SellerAgg;

namespace KBA.Domain.Entity.SellerLogin
{
    public class SellerLoginReport : BaseEntity
    {
        public DateTime LoginTime { get; private set; }
        public DateTime? LogoutTime { get; private set; }
        public string IpAddress { get; private set; }
        public int SellerId { get; private set; }

        #region Relation

        public Seller Seller { get; private set; }

        #endregion

        #region Constructor

        public SellerLoginReport(string ipAddress, int sellerId)
        {
            LoginTime = DateTime.UtcNow;
            IpAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
            SellerId = sellerId;
        }
        public void UpdateIpAddress(string newIpAddress)
        {
            if (string.IsNullOrEmpty(newIpAddress))
            {
                throw new ArgumentException("IP address cannot be null or empty.");
            }

            IpAddress = newIpAddress;
            LastUpdateDate = DateTime.UtcNow;
        }

        public void SetLogoutTime()
        {
            LogoutTime = DateTime.UtcNow;
        }
        #endregion
    }
}
