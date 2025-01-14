using FluentValidation;
using KBA.SellerApplication.Contract.Auth.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Auth.Validation
{
    public class SellerLoginReportValidator : AbstractValidator<SellerLoginReportDto>
    {
        public SellerLoginReportValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("شماره تلفن نمی‌تواند خالی باشد.")
                .Matches(@"^\d{11}$").WithMessage("شماره تلفن باید 11 رقم باشد.");

            RuleFor(x => x.IpAddress)
                .NotEmpty().WithMessage("آدرس IP نمی‌تواند خالی باشد.")
                .Matches(@"^(25[0-5]|2[0-4][0-9]|[0-1]?[0-9]{1,2})(\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9]{1,2})){3}$")
                .WithMessage("آدرس IP باید معتبر باشد.");
        }
    }

}
