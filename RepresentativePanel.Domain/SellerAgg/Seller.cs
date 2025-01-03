﻿using KermanBattery.Farmework.Domain;
using RepresentativePanel.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Domain.SellerAgg
{
    public class Seller : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string NationalNumber { get; private set; }
        public string OtpCode { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Title { get; private set; }
        public string Province { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsVisible { get; private set; }
        public bool IsTropical { get; private set; }
        public string Grade { get; private set; }
        public long? ParentId { get; private set; }
        public bool IsSubmitOrder { get; private set; }
        public DateTime CodeExpireTime { get; private set; }
        public Gender Gender { get; private set; }
        public Roles Role { get; private set; }

        public void ChangePassword(string newPassword, ISellerRepository hasher)
        {
            Password = hasher.HashPassword(newPassword);
        }
        public void SetOtpCode(string otpCode, DateTime expireTime)
        {
            OtpCode = otpCode;
            CodeExpireTime = expireTime;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
        public Seller(string phoneNumber, Roles role, bool isDeleted)
        {
            PhoneNumber = phoneNumber;
            Role = role;
            IsDeleted = isDeleted;
            IsActive = true;
        }
    }
}
