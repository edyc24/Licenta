using AJFIlfov.BusinessLogic;
using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.Account;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateService;
using AJFIlfov.BusinessLogic.Implementation.EchipeService;
using AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService;
using AJFIlfov.BusinessLogic.Implementation.GrupeService;
using AJFIlfov.BusinessLogic.Implementation.LocalitatiService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService;
using AJFIlfov.BusinessLogic.Implementation.StadioaneService;
using AJFIlfov.BusinessLogic.Implementation.StadionLocalitateService;
using AJFIlfov.BusinessLogic.Implementation.User;
using AJFIlfov.Code.Base;
using AJFIlfov.Common;
using AJFIlfov.Common.DTOs;
using AJFIlfov.Services;
using AJFIlfov.WebApp.Code.Base;
using System.Security.Claims;

namespace AJFIlfov.WebApp.Code.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ControllerDependencies>();

            return services;
        }

        public static IServiceCollection AddAJFIlfovBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<AccountService>();
            services.AddScoped<MailService>();
            services.AddScoped<UsersService>();
            services.AddScoped<DisponibilitateService>();
            services.AddScoped<DisponibilitateAdminService>();
            services.AddScoped<MeciuriService>();
            services.AddScoped<EchipeService>();
            services.AddScoped<MatchReportService>();
            services.AddScoped<GrupeService>();
            services.AddScoped<GrupeEchipaService>();
            services.AddScoped<StadioaneService>();
            services.AddScoped<LocalitatiService>();
            services.AddScoped<StadionLocalitateService>();
            return services;
        }

        public static IServiceCollection AddAJFIlfovCurrentUser(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                var claims = httpContext.User.Claims;

                var userIdClaim = claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
                var userEmailClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var userRoleClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                var isParsingSuccessful = Guid.TryParse(userIdClaim, out Guid id);

                var currentUser = new CurrentUserDto
                {
                    Id = id,
                    IsAuthenticated = httpContext.User.Identity.IsAuthenticated,
                    FirstName = httpContext.User.Identity.Name,
                    Email = userEmailClaim,
                    Role = userRoleClaim
                };
                return currentUser;
            });

            return services;
        }
    }
}
