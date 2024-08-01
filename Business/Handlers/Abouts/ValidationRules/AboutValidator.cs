
using Business.Handlers.Abouts.Commands;
using FluentValidation;

namespace Business.Handlers.Abouts.ValidationRules
{

    public class CreateAboutValidator : AbstractValidator<CreateAboutCommand>
    {
        public CreateAboutValidator()
        {
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.BannerUrl).NotEmpty();

        }
    }
    public class UpdateAboutValidator : AbstractValidator<UpdateAboutCommand>
    {
        public UpdateAboutValidator()
        {
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.BannerUrl).NotEmpty();

        }
    }
}