using Application.DTO;
using Application.ErrorResponses;
using Application.Exceptions;
using AutoMapper;
using FluentValidation;
using Infrastructure.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.Comment.Comman
{
    public class Create
    {
        public class Command : IRequest<Result<CommentDto>>
        {
            public string Body { get; set; }
            public Guid ActivityId { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Body).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, Result<CommentDto>>
        {
            private readonly DataContext db;
            private readonly IUserAccessor userAccessor;
            private readonly IMapper mapper;
            public Handler(DataContext db, IUserAccessor userAccessor, IMapper mapper)
            {
                this.db = db;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
            }
            public async Task<Result<CommentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await db.Activities.FindAsync(request.ActivityId);
                if (activity == null) throw new BadRequestException("Not Found ActivityId ");
                var user = await db.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());
                var comment = new Domain.Comment
                {
                    Authot = user,
                    Activity = activity,
                    Body = request.Body
                };

                activity.Comments.Add(comment);
                var success = await db.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Result<CommentDto>.Success(mapper.Map<CommentDto>(comment));
                return Result<CommentDto>.Failure("Failed to add comment");
            }
        }
    }
}
