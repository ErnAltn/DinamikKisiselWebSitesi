
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
using Business.Handlers.Articles.ValidationRules;


namespace Business.Handlers.Articles.Commands
{


    public class UpdateArticleCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public string Header { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }

        public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, IResult>
        {
            private readonly IArticleRepository _articleRepository;
            private readonly IMediator _mediator;

            public UpdateArticleCommandHandler(IArticleRepository articleRepository, IMediator mediator)
            {
                _articleRepository = articleRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateArticleValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
            {
                var isThereArticleRecord = await _articleRepository.GetAsync(u => u.Id == request.Id);


                isThereArticleRecord.CreatedDate = request.CreatedDate;
                isThereArticleRecord.UpdatedDate = request.UpdatedDate;
                isThereArticleRecord.DeletedDate = request.DeletedDate;
                isThereArticleRecord.Header = request.Header;
                isThereArticleRecord.ImageUrl = request.ImageUrl;
                isThereArticleRecord.Description = request.Description;
                isThereArticleRecord.Subject = request.Subject;


                _articleRepository.Update(isThereArticleRecord);
                await _articleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

