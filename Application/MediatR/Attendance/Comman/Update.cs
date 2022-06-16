using Application.ErrorResponses;
using Application.Exceptions;
using Infrastructure.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

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

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                this.context = context;
                this.userAccessor = userAccessor;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities
                    .Include(a => a.Attendees)
                    .ThenInclude(a => a.AppUser)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);


                if (activity == null) throw new BadRequestException("Id not found ddatabase");

                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());//Hal-hazirda sistemde hansu user oldugnu tapir

                if (user == null) throw new BadRequestException("user not found database");

                var hostUsername = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;//kimin activity oldugnu tapir hostUsername

                var attendence = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

                if (attendence != null && hostUsername == user.UserName)
                    activity.IsCancelled = !activity.IsCancelled;

                if (attendence != null && hostUsername != user.UserName)
                    activity.Attendees.Remove(attendence);

                if (attendence == null)
                {
                    attendence = new Domain.ActivityAttendee
                    {
                        AppUser = user,
                        Activity = activity,
                        IsHost = false
                    };
                    activity.Attendees.Add(attendence);
                }
                var result = await context.SaveChangesAsync() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating attendaca");
            }
        }
    }
}
