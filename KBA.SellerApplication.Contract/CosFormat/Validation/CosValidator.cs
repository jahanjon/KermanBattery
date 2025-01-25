using FluentValidation;
using KBA.SellerApplication.Contract.CosFormat.ViewModel;

namespace KBA.SellerApplication.Contract.CosFormat.Validation
{
    public class CosFormatValidator : AbstractValidator<CosFormatViewModel>
    {
        public CosFormatValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("عنوان نمی‌تواند خالی باشد.")
                .MaximumLength(100).WithMessage("عنوان نمی‌تواند بیشتر از 100 کاراکتر باشد.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("توضیحات نمی‌تواند خالی باشد.")
                .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد.");
        }
    }

}
