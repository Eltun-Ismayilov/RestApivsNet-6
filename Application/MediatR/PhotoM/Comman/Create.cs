using Application.ErrorResponses;
using Domain;
using Infrastructure.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.PhotoM.Comman
{
    public class Create
    {
        public class Command : IRequest<Result<Photo>>
        {
            public IFormFile File { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Photo>>
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
            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await db.Users.Include(x => x.Photos)
                    .FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());
                if (user == null) return null;
                var photoUpload = await photoAccessor.AddPhoto(request.File);
                var photo = new Photo
                {
                    Url = photoUpload.Url,
                    Id = photoUpload.PublicId,
                };
                if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;
                db.Photos.Add(photo);
                var result = await db.SaveChangesAsync() > 0;
                if (result)
                {
                    return Result<Photo>.Success(photo);
                }

                return Result<Photo>.Failure("Problem Adding photo");
            }
        }
    }
}   
