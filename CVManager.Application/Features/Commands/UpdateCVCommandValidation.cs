
namespace CVManager.Application.Features.Commands
{
    public class UpdateCVCommandValidation : AbstractValidator<AddCVCommand>
    {
        public UpdateCVCommandValidation()
        {
            RuleFor(x => x.CVRequest.Name).NotNull().NotEmpty()
                       .WithMessage("Name Is Required");

            RuleFor(x => x.CVRequest.FullName).NotNull().NotEmpty()
                       .WithMessage("FullName Is Required");

            RuleFor(x => x.CVRequest.Email).Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
                       .WithMessage("Email InVaild");

            RuleFor(x => x.CVRequest.MobileNumber).Matches("^(0|[0-9][0-9]*)$")
                       .WithMessage("MobileNumber InVaild");

            RuleFor(x => x.CVRequest.CompanyName).NotNull().NotEmpty().WithMessage("CompanyName Is Required")
                       .MaximumLength(20)
                       .WithMessage("CompanyName Max Length 20");
        }
    }
}
