using Application.DTO;
using Application.Exceptions;
using Application.TokenServiceProvider;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.AccountM.Comman
{
    public class Register
    {
        public class Command : IRequest<UserDto>
        {
            public RegisterDto RegisterDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly UserManager<AppUser> userManager;
            private readonly TokenService tokenService;
            public Handler(UserManager<AppUser> userManager, TokenService tokenService)
            {
                this.userManager = userManager;
                this.tokenService = tokenService;
            }

            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await userManager.Users.AnyAsync(x => x.Email == request.RegisterDto.Email))
                {
                   throw new BadRequestException("Email taken");
                }

                if (await userManager.Users.AnyAsync(x => x.UserName == request.RegisterDto.Username))
                {
                    throw new BadRequestException("UserName taken");
                }
                var user = new AppUser
                {
                    DisplayName = request.RegisterDto.DisplayName,
                    Email = request.RegisterDto.Email,
                    UserName = request.RegisterDto.Username
                };

                var result = await userManager.CreateAsync(user, request.RegisterDto.Password);
                if (result.Succeeded)
                {

                    return new UserDto
                    {
                        DisplayName = user.DisplayName,
                        Image = null,
                        Token = tokenService.CreateToken(user),
                        Username = user.UserName
                    };
                }
                throw new BadRequestException("Problem registering user");
            }
        }
    }
}
