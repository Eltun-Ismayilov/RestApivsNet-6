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
    public class List
    {
        public class Query:IRequest<List<Activity>> { }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }
            public async  Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Activities.Where(a=>a.DeleteData==null).ToListAsync(cancellationToken);
            }
        }
    }
}
