
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Meetings.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMeetingCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, IResult>
        {
            private readonly IMeetingRepository _meetingRepository;
            private readonly IMediator _mediator;

            public DeleteMeetingCommandHandler(IMeetingRepository meetingRepository, IMediator mediator)
            {
                _meetingRepository = meetingRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
            {
                var meetingToDelete = _meetingRepository.Get(p => p.Id == request.Id);

                _meetingRepository.Delete(meetingToDelete);
                await _meetingRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

