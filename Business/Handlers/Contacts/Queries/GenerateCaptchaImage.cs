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

namespace Business.Handlers.Contacts.Queries
{
    public class GenerateCaptchaImage : IRequest<IResult>
    {
        public class GenerateCaptchaImageHandler : IRequestHandler<GenerateCaptchaImage, IResult>
        {
            private readonly IContactRepository _contactRepository;
            private readonly IMediator _mediator;

            public GenerateCaptchaImageHandler(IContactRepository contactRepository, IMediator mediator)
            {
                _contactRepository = contactRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            public async Task<IResult> Handle(GenerateCaptchaImage request, CancellationToken cancellationToken)
            {
                var result = _contactRepository.CaptchaGeneratorAsync();

                return new SuccessResult(result);
            }
        }
    }
}
