using FluentValidation;
using KBA.SellerApplication.Contract.Auth.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Auth.Validation
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("شماره تلفن نمی‌تواند خالی باشد.")
                .Matches(@"^\d{11}$").WithMessage("شماره تلفن باید 11 رقم باشد.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("رمز عبور نمی‌تواند خالی باشد.")
                .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد.");

            RuleFor(x => x.IPAddress)
                .NotEmpty().WithMessage("آدرس IP نمی‌تواند خالی باشد.")
                .Matches(@"^(25[0-5]|2[0-4][0-9]|[0-1]?[0-9]{1,2})(\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9]{1,2})){3}$")
                .WithMessage("آدرس IP باید معتبر باشد.");
        }
    }
}
