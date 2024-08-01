
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Meetings.Queries
{

    public class GetMeetingsQuery : IRequest<IDataResult<IEnumerable<Meeting>>>
    {
        public class GetMeetingsQueryHandler : IRequestHandler<GetMeetingsQuery, IDataResult<IEnumerable<Meeting>>>
        {
            private readonly IMeetingRepository _meetingRepository;
            private readonly IMediator _mediator;

            public GetMeetingsQueryHandler(IMeetingRepository meetingRepository, IMediator mediator)
            {
                _meetingRepository = meetingRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Meeting>>> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Meeting>>(await _meetingRepository.GetListAsync());
            }
        }
    }
}