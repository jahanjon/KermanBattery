﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KermanBattery.Farmework.Domain;
using RepresentativePanel.Domain.Entity.SellerAgg;

namespace RepresentativePanel.Domain.Entity.SellerLogin
{
    public class SellerLogin :BaseEntity
    {
        public string PhoneNumber { get; private set; }
        public DateTime LoginTime { get; private set; }
        public DateTime? LogoutTime { get; private set; }
        public string IpAddress { get; private set; }
        public int SellerId { get; private set; }

        #region Relation

        public Seller Seller { get; private set; }

        #endregion

        #region Constructor

        public SellerLogin(string phoneNumber, string ipAddress, int sellerId)
        {
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
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
            if (LogoutTime != null)
                throw new InvalidOperationException("Logout time is already set.");
            LogoutTime = DateTime.UtcNow;
        }
        #endregion
    }
}
