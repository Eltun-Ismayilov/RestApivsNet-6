using Application.ErrorResponses;
using Application.Validation;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ActivityM.Comman
{
    public class Update
    {

        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {

                var activity = await context.Activities.FirstOrDefaultAsync(a=>a.Id==request.Activity.Id && a.DeleteData==null);
                if (activity == null) return null;
                request.Activity.UpdateData = DateTime.UtcNow;

                mapper.Map(request.Activity,activity);
                var result=await context.SaveChangesAsync(cancellationToken)>0;
                if (!result) return Result<Unit>.Failure("Failed to update  activitty");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
