using Application.ErrorResponses;
using Infrastructure.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.PhotoM.Comman
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext db;
            private readonly IUserAccessor userAccessor;
            private readonly IPhotoAccessor photoAccessor;
            public Handler(DataContext db, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
            {
                this.db = db;
                this.userAccessor = userAccessor;
                this.photoAccessor = photoAccessor;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await db.Users.Include(p => p.Photos)
                    .FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());
                if (user == null) return null;
                var photo = db.Photos.FirstOrDefault(x => x.Id == request.Id);
                if (photo == null) return null;
                if (!photo.IsMain) return Result<Unit>.Failure("You cannot delete your main photo");
                var result = await photoAccessor.DeletePhoto(photo.Id);
                if (result == null) return Result<Unit>.Failure("Problem deleting photo from Cloudinary");
                user.Photos.Remove(photo);
                var seccuss = await db.SaveChangesAsync() > 0;
                if (!seccuss) return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Problem deleting photo from API");
            }
        }
    }
}
