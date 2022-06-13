using Application.ErrorResponses;
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
        public class Query : IRequest<Result<Activity>> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Activity>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }
            public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity= await context.Activities.FirstOrDefaultAsync(a=>a.Id==request.Id && a.DeleteData==null);
                return Result<Activity>.Success(activity);
            }
        }
    }
}
