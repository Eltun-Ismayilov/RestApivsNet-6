using Application.ErrorResponses;
using Application.VM;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ActivityM.Query
{
    public class List
    {
        public class Query:IRequest<Result<List<ActivityVm>>> { }

        public class Handler : IRequestHandler<Query, Result<List<ActivityVm>>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async  Task<Result<List<ActivityVm>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await context.Activities
                    .Include(a=>a.Attendees)
                    .ThenInclude(a=>a.AppUser)
                    .ToListAsync(cancellationToken);

                var vm=mapper.Map<List<ActivityVm>>(activities);

                return Result<List<ActivityVm>>.Success(vm);
            }
        }
    }
}
