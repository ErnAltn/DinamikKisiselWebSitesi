
using Business.Handlers.Articles.Commands;
using FluentValidation;

namespace Business.Handlers.Articles.ValidationRules
{

    public class CreateArticleValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Subject).NotEmpty();

        }
    }
    public class UpdateArticleValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Subject).NotEmpty();

        }
    }
}