﻿using KBA.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Seller
{
    public class DashboardDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NationalNumber { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string Title { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string Grade { get; set; }

    }
}
