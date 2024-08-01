
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


namespace Business.Handlers.Articles.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteArticleCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, IResult>
        {
            private readonly IArticleRepository _articleRepository;
            private readonly IMediator _mediator;

            public DeleteArticleCommandHandler(IArticleRepository articleRepository, IMediator mediator)
            {
                _articleRepository = articleRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
            {
                var articleToDelete = _articleRepository.Get(p => p.Id == request.Id);

                _articleRepository.Delete(articleToDelete);
                await _articleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

