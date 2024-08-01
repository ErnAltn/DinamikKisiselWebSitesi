
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Meetings.Queries
{
    public class GetMeetingQuery : IRequest<IDataResult<Meeting>>
    {
        public int Id { get; set; }

        public class GetMeetingQueryHandler : IRequestHandler<GetMeetingQuery, IDataResult<Meeting>>
        {
            private readonly IMeetingRepository _meetingRepository;
            private readonly IMediator _mediator;

            public GetMeetingQueryHandler(IMeetingRepository meetingRepository, IMediator mediator)
            {
                _meetingRepository = meetingRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Meeting>> Handle(GetMeetingQuery request, CancellationToken cancellationToken)
            {
                var meeting = await _meetingRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Meeting>(meeting);
            }
        }
    }
}
