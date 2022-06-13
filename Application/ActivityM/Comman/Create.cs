using Application.ErrorResponses;
using Application.Validation;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ActivityM.Comman
{
    public class Create
    {
        public class Command :IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x=>x.Activity).SetValidator(new ActivityValidator());
            }
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
                request.Activity.CreateData = DateTime.Now;
                await context.Activities.AddAsync(request.Activity);
                var result=await context.SaveChangesAsync(cancellationToken)>0;
                if (!result) return Result<Unit>.Failure("Failed to create activity");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
