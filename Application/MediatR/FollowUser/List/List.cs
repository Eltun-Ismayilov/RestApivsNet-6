using Application.ErrorResponses;
using Application.VM;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.FollowUser.List
{
    public class List
    {
        public class Query : IRequest<Result<List<VM.ProfileVm>>>
        {
            public string Predicate { get; set; }
            public string Username { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<VM.ProfileVm>>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;
            private readonly IUserAccessor userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                this.context = context;
                this.mapper = mapper;
                this.userAccessor = userAccessor;
            }

            public async Task<Result<List<ProfileVm>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var profiles = new List<VM.ProfileVm>();

                switch (request.Predicate)
                {
                    case "followers":
                        profiles = await context.UserFollowings.Where(x => x.Target.UserName == request.Username)
                            .Select(x => x.Observer)
                            .ProjectTo<VM.ProfileVm>(mapper.ConfigurationProvider, new { currentUsername = userAccessor.GetUsername() })
                            .ToListAsync();
                        break;

                    case "following":
                        profiles = await context.UserFollowings.Where(x => x.Observer.UserName == request.Username)
                            .Select(x => x.Target)
                            .ProjectTo<VM.ProfileVm>(mapper.ConfigurationProvider)
                            .ToListAsync();
                        break;

                }
                return Result<List<VM.ProfileVm>>.Success(profiles);
            }
        }
    }
}
