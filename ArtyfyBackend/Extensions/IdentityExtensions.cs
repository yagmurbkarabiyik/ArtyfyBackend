using ArtyfyBackend.Bll.Constants;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ArtyfyBackend.API.Extensions
{
    public static class IdentityExtension
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            //Identity Configurations
            services.AddIdentity<UserApp, IdentityRole>(Opt =>
            {
                Opt.User.RequireUniqueEmail = true;

                Opt.Password.RequiredLength = 6;
                Opt.Password.RequireLowercase = true;
                Opt.Password.RequireUppercase = false;
                Opt.Password.RequireNonAlphanumeric = false;
                Opt.Password.RequireDigit = false;

            })
                .AddErrorDescriber<ErrorDescriber>()
                .AddEntityFrameworkStores<ArtyfyBackendDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}