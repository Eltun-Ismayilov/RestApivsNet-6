﻿using Domain;
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
        public class Query : IRequest<Activity> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Activity>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }
            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Activities.FirstOrDefaultAsync(a=>a.Id==request.Id && a.DeleteData==null);
            }
        }
    }
}
