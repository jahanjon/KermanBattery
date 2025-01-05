using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Contract.Auth.Dto
{
    public class TokenParametersDto
    {
        public string PhoneNumber { get; set; }
        public int Role { get; set; }
    }
}
