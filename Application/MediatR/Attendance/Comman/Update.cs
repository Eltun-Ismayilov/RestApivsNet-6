using Application.ErrorResponses;
using Application.Exceptions;
using Infrastructure.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.Attendance.Comman
{
    public class Update
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext context;
            private readonly IUserAccessor userAccessor;

            public Handler(DataContext context,IUserAccessor userAccessor)
            {
                this.context = context;
                this.userAccessor = userAccessor;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities.Include(a => a.Attendees).ThenInclude(a => a.AppUser).SingleOrDefaultAsync(x => x.Id == request.Id);

                if (activity == null) throw new BadRequestException("Id not found ddatabase");

                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());

                if (user == null) throw new BadRequestException("user not found ddatabase");

                var hostUsername=activity.Attendees.FirstOrDefault(x=>x.IsHost)?.AppUser?.UserName;

                var attendence=activity.Attendees.FirstOrDefault(x=>x.AppUser.UserName==user.UserName);

                if(attendence!=null )

            }
        }
    }
}
