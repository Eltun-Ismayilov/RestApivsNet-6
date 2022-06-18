using Application.ActivityM.Query;
using Application.AutoMapper;
using Application.TokenServiceProvider;
using Domain;
using Infrastructure.Interface;
using Infrastructure.Photos;
using Infrastructure.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("cString")));

            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            
            return services;
        }
    }
}
