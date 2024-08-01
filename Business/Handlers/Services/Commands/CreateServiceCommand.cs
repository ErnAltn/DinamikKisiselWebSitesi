
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
using Business.Handlers.Services.ValidationRules;

namespace Business.Handlers.Services.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateServiceCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public string Header { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }


        public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, IResult>
        {
            private readonly IServiceRepository _serviceRepository;
            private readonly IMediator _mediator;
            public CreateServiceCommandHandler(IServiceRepository serviceRepository, IMediator mediator)
            {
                _serviceRepository = serviceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateServiceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
            {
                var addedService = new Service
                {
                    CreatedDate = request.CreatedDate,
                    UpdatedDate = request.UpdatedDate,
                    DeletedDate = request.DeletedDate,
                    Header = request.Header,
                    ImageUrl = request.ImageUrl,
                    Description = request.Description,

                };

                _serviceRepository.Add(addedService);
                await _serviceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}