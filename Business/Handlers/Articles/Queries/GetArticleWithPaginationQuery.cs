using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Articles.Queries
{
    public class GetArticlesWithPaginationQuery : IRequest<Paginate<Article>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public class GetArticlesWithPaginationQueryHandler : IRequestHandler<GetArticlesWithPaginationQuery, Paginate<Article>>
        {
            private readonly IArticleRepository _articleRepository;
            private readonly IMediator _mediator;

            public GetArticlesWithPaginationQueryHandler(IArticleRepository articleRepository, IMediator mediator)
            {
                _articleRepository = articleRepository;
                _mediator = mediator;
            }
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [AllowAnonymous]


            public async Task<Paginate<Article>> Handle(GetArticlesWithPaginationQuery request, CancellationToken cancellationToken)
            {
                return _articleRepository.GetPagedAsync(request.PageNumber, request.PageSize);
            }
        }
    }
}
