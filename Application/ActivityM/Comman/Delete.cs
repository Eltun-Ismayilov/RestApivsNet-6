using Application.ErrorResponses;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ActivityM.Comman
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity= await context.Activities.FindAsync(request.Id);
                activity.DeleteData = DateTime.UtcNow;
                if (activity == null)  return null;
              var result=  await context.SaveChangesAsync(cancellationToken)>0;
                if (!result) return Result<Unit>.Failure("Failed to delete the activitty");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
