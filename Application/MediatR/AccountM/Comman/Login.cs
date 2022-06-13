using Application.DTO;
using Application.Exceptions;
using Application.TokenServiceProvider;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.AccountM.Comman
{
    public class Login
    {
        public class Command : IRequest<UserDto>
        {
            public LoginDto LoginDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly UserManager<AppUser> userManager;
            private readonly SignInManager<AppUser> signInManager;
            private readonly TokenService tokenService;
            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.tokenService = tokenService;
            }

            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByEmailAsync(request.LoginDto.Email);
                if (user == null) throw new BadRequestException("Email and password not correct.");
                var result = await signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);
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

                throw new NotFoundException(nameof(AppUser), request.LoginDto.Email);

            }
        }
    }
}
