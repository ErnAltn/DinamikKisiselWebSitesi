
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Articles.Queries
{
    public class GetArticleQuery : IRequest<IDataResult<Article>>
    {
        public int Id { get; set; }

        public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, IDataResult<Article>>
        {
            private readonly IArticleRepository _articleRepository;
            private readonly IMediator _mediator;

            public GetArticleQueryHandler(IArticleRepository articleRepository, IMediator mediator)
            {
                _articleRepository = articleRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Article>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
            {
                var article = await _articleRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Article>(article);
            }
        }
    }
}
