
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

namespace Business.Handlers.Articles.Queries
{

    public class GetArticlesQuery : IRequest<IDataResult<IEnumerable<Article>>>
    {
        public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, IDataResult<IEnumerable<Article>>>
        {
            private readonly IArticleRepository _articleRepository;
            private readonly IMediator _mediator;

            public GetArticlesQueryHandler(IArticleRepository articleRepository, IMediator mediator)
            {
                _articleRepository = articleRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Article>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Article>>(await _articleRepository.GetListAsync());
            }
        }
    }
}