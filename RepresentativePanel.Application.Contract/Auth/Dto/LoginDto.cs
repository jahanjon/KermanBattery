﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Contract.Auth.Dto
{
    public class LoginDto
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
    }
}
