using Application.ErrorResponses;
using Application.Pagination;
using Application.VM;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Infrastructure.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ActivityM.Query
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ActivityDTO>>> { public PagingParams Params { get; set; }}

        public class Handler : IRequestHandler<Query, Result<PagedList<ActivityDTO>>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;
            private readonly IUserAccessor userAccessor;

            public Handler(DataContext context,IMapper mapper,IUserAccessor userAccessor)
            {
                this.context = context;
                this.mapper = mapper;
                this.userAccessor = userAccessor;
            }
            public async  Task<Result<PagedList<ActivityDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = context.Activities
                    .OrderBy(d => d.CreateData)
                    .ProjectTo<ActivityDTO>(mapper.ConfigurationProvider,
                        new { currentUsername = userAccessor.GetUsername() })
                    .AsQueryable();
                var vm=mapper.Map<List<ActivityDTO>>(query);

                return Result<PagedList<ActivityDTO>>.Success(await PagedList<ActivityDTO>.CreateAsync(query,request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}
