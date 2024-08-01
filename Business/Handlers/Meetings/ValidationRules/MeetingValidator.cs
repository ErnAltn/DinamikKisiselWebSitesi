
using Business.Handlers.Meetings.Commands;
using FluentValidation;

namespace Business.Handlers.Meetings.ValidationRules
{

    public class CreateMeetingValidator : AbstractValidator<CreateMeetingCommand>
    {
        public CreateMeetingValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Time).NotEmpty();
            RuleFor(x => x.ShortInfo).NotEmpty();

        }
    }
    public class UpdateMeetingValidator : AbstractValidator<UpdateMeetingCommand>
    {
        public UpdateMeetingValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Time).NotEmpty();
            RuleFor(x => x.ShortInfo).NotEmpty();

        }
    }
}