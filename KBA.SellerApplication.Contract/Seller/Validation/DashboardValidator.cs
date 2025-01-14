using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.Seller.Validation
{

    public class DashboardValidator : AbstractValidator<DashboardDto>
    {
        public DashboardValidator()
        {
            //RuleFor(x => x.SellerId)
            //    .GreaterThan(0).WithMessage("کد فروشنده باید بزرگتر از 0 باشد.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام نمی‌تواند خالی باشد.")
                .MaximumLength(50).WithMessage("نام نمی‌تواند بیشتر از 50 کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی نمی‌تواند خالی باشد.")
                .MaximumLength(50).WithMessage("نام خانوادگی نمی‌تواند بیشتر از 50 کاراکتر باشد.");

            RuleFor(x => x.Email).EmailAddress().WithMessage("ایمیل باید معتبر باشد.");

            RuleFor(x => x.NationalNumber)
                .NotEmpty().WithMessage("شماره ملی نمی‌تواند خالی باشد.")
                .Length(10).WithMessage("شماره ملی باید دقیقا 10 رقم باشد.")
                .Matches(@"^\d+$").WithMessage("شماره ملی فقط باید شامل اعداد باشد.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("شماره تلفن نمی‌تواند خالی باشد.")
                .Matches(@"^\d{11}$").WithMessage("شماره تلفن باید 11 رقم باشد.");

            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("عنوان نمی‌تواند بیشتر از 100 کاراکتر باشد.");

            RuleFor(x => x.Province)
                .MaximumLength(50).WithMessage("استان نمی‌تواند بیشتر از 50 کاراکتر باشد.");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("آدرس نمی‌تواند بیشتر از 200 کاراکتر باشد.");

            RuleFor(x => x.City)
                .MaximumLength(50).WithMessage("شهر نمی‌تواند بیشتر از 50 کاراکتر باشد.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد.");

            RuleFor(x => x.Grade)
                .NotEmpty().WithMessage("درجه نمی‌تواند خالی باشد.")
                .MaximumLength(50).WithMessage("درجه نمی‌تواند بیشتر از 50 کاراکتر باشد.");

            RuleFor(x => x.Gender)  
          .IsInEnum().WithMessage("لطفاً یکی از گزینه‌های معتبر جنسیت را انتخاب کنید.");
        }

    }
}