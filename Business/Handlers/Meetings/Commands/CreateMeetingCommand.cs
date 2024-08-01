
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Meetings.ValidationRules;

namespace Business.Handlers.Meetings.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMeetingCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public System.DateTime Date { get; set; }
        public string Time { get; set; }
        public string ShortInfo { get; set; }


        public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, IResult>
        {
            private readonly IMeetingRepository _meetingRepository;
            private readonly IMediator _mediator;
            public CreateMeetingCommandHandler(IMeetingRepository meetingRepository, IMediator mediator)
            {
                _meetingRepository = meetingRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateMeetingValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
            {

                var addedMeeting = new Meeting
                {
                    CreatedDate = request.CreatedDate,
                    UpdatedDate = request.UpdatedDate,
                    DeletedDate = request.DeletedDate,
                    NameSurname = request.NameSurname,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Date = request.Date,
                    Time = request.Time,
                    ShortInfo = request.ShortInfo,

                };

                _meetingRepository.Add(addedMeeting);
                await _meetingRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}