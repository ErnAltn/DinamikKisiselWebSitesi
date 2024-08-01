
using Business.Handlers.Services.Commands;
using FluentValidation;

namespace Business.Handlers.Services.ValidationRules
{

    public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();

        }
    }
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();

        }
    }
}