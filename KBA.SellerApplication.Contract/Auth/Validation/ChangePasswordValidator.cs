using FluentValidation;
using KBA.SellerApplication.Contract.Auth.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Auth.Validation
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("شماره تلفن نمی‌تواند خالی باشد.")
                .Matches(@"^\d{11}$").WithMessage("شماره تلفن باید 11 رقم باشد.");

            RuleFor(x => x.VerificationCode)
                .NotEmpty().WithMessage("کد تایید نمی‌تواند خالی باشد.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("رمز عبور جدید نمی‌تواند خالی باشد.")
                .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
                .WithMessage("رمز عبور باید شامل حروف بزرگ، کوچک و عدد باشد.");
        }
    }

}
