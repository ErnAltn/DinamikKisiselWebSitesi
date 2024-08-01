
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
using Business.Handlers.Services.ValidationRules;


namespace Business.Handlers.Services.Commands
{


    public class UpdateServiceCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public string Header { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, IResult>
        {
            private readonly IServiceRepository _serviceRepository;
            private readonly IMediator _mediator;

            public UpdateServiceCommandHandler(IServiceRepository serviceRepository, IMediator mediator)
            {
                _serviceRepository = serviceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateServiceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
            {
                var isThereServiceRecord = await _serviceRepository.GetAsync(u => u.Id == request.Id);


                isThereServiceRecord.CreatedDate = request.CreatedDate;
                isThereServiceRecord.UpdatedDate = request.UpdatedDate;
                isThereServiceRecord.DeletedDate = request.DeletedDate;
                isThereServiceRecord.Header = request.Header;
                isThereServiceRecord.ImageUrl = request.ImageUrl;
                isThereServiceRecord.Description = request.Description;


                _serviceRepository.Update(isThereServiceRecord);
                await _serviceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

