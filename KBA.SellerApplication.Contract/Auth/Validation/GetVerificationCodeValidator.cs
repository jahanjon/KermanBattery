using FluentValidation;
using KBA.SellerApplication.Contract.Auth.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Auth.Validation
{
    public class GetVerificationCodeValidator : AbstractValidator<GetverificationCodeDto>
    {
        public GetVerificationCodeValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("شماره تلفن نمی‌تواند خالی باشد.")
                .Matches(@"^\d{11}$").WithMessage("شماره تلفن باید 11 رقم باشد.");
        }
    }

}
