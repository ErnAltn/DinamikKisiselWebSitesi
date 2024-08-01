
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
using Business.Handlers.Articles.ValidationRules;

namespace Business.Handlers.Articles.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateArticleCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public string Header { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }


        public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, IResult>
        {
            private readonly IArticleRepository _articleRepository;
            private readonly IMediator _mediator;
            public CreateArticleCommandHandler(IArticleRepository articleRepository, IMediator mediator)
            {
                _articleRepository = articleRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateArticleValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
            {
                
                var addedArticle = new Article
                {
                    CreatedDate = request.CreatedDate,
                    UpdatedDate = request.UpdatedDate,
                    DeletedDate = request.DeletedDate,
                    Header = request.Header,
                    ImageUrl = request.ImageUrl,
                    Description = request.Description,
                    Subject = request.Subject,

                };

                _articleRepository.Add(addedArticle);
                await _articleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}