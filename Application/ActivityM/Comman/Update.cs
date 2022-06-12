using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ActivityM.Comman
{
    public class Update
    {

        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var activity = await context.Activities.FirstOrDefaultAsync(a=>a.Id==request.Activity.Id && a.DeleteData==null);
                request.Activity.UpdateData = DateTime.UtcNow;
                mapper.Map(request.Activity,activity);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
