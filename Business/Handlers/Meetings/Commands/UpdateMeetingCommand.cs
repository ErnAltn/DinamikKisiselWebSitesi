
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Meetings.ValidationRules;


namespace Business.Handlers.Meetings.Commands
{


    public class UpdateMeetingCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public System.DateTime Date { get; set; }
        public string Time { get; set; }
        public string ShortInfo { get; set; }

        public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand, IResult>
        {
            private readonly IMeetingRepository _meetingRepository;
            private readonly IMediator _mediator;

            public UpdateMeetingCommandHandler(IMeetingRepository meetingRepository, IMediator mediator)
            {
                _meetingRepository = meetingRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateMeetingValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
            {
                var isThereMeetingRecord = await _meetingRepository.GetAsync(u => u.Id == request.Id);


                isThereMeetingRecord.CreatedDate = request.CreatedDate;
                isThereMeetingRecord.UpdatedDate = request.UpdatedDate;
                isThereMeetingRecord.DeletedDate = request.DeletedDate;
                isThereMeetingRecord.NameSurname = request.NameSurname;
                isThereMeetingRecord.Email = request.Email;
                isThereMeetingRecord.PhoneNumber = request.PhoneNumber;
                isThereMeetingRecord.Date = request.Date;
                isThereMeetingRecord.Time = request.Time;
                isThereMeetingRecord.ShortInfo = request.ShortInfo;


                _meetingRepository.Update(isThereMeetingRecord);
                await _meetingRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

