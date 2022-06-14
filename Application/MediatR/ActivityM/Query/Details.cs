using Application.ErrorResponses;
using Application.VM;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ActivityM.Query
{
    public class Details
    {
        public class Query : IRequest<Result<ActivityVm>> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ActivityVm>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<Result<ActivityVm>> Handle(Query request, CancellationToken cancellationToken)
            {

                var activities = await context.Activities
                   .Include(a => a.Attendees)
                   .ThenInclude(a => a.AppUser)
                   .FirstOrDefaultAsync(x=>x.Id==request.Id);

                var vm = mapper.Map<ActivityVm>(activities);

                return Result<ActivityVm>.Success(vm);
            }
        }
    }
}
