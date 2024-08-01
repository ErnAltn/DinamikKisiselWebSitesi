using Business.Handlers.Contacts.Queries;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Meetings.Queries
{
    public class GenerateCaptchaImageMeeting : IRequest<IResult>
    {
        public class GenerateCaptchaImageMeetingHandler : IRequestHandler<GenerateCaptchaImageMeeting, IResult>
        {
            private readonly IMeetingRepository _meetingRepository;
            private readonly IMediator _mediator;

            public GenerateCaptchaImageMeetingHandler(IMeetingRepository meetingRepository, IMediator mediator)
            {
                _meetingRepository = meetingRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            public async Task<IResult> Handle(GenerateCaptchaImageMeeting request, CancellationToken cancellationToken)
            {
                var result = _meetingRepository.CaptchaGeneratorAsync();

                return new SuccessResult(result);
            }
        }
    }
}
