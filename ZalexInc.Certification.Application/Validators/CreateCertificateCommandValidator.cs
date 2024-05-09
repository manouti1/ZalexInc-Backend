using FluentValidation;
using ZalexInc.Certification.Application.Commands;

namespace ZalexInc.Certification.Application.Validators
{
    public class CreateCertificateCommandValidator : AbstractValidator<CreateCertificateCommand>
    {
        public CreateCertificateCommandValidator()
        {
            RuleFor(x => x.AddressTo).NotEmpty().WithMessage("Address To is required");
            RuleFor(x => x.Purpose).NotEmpty().WithMessage("Purpose is required");
            RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Employee Id is required");
            RuleFor(x => x.IssuedOn).NotEmpty().WithMessage("Issued On is required");
        }
    }
}
