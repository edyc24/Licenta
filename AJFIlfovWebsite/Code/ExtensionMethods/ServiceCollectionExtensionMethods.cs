using System.Security.Claims;
using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.Account;
using AJFIlfov.BusinessLogic.Implementation.Anunturi;
using AJFIlfov.BusinessLogic.Implementation.Audituri;
using AJFIlfov.BusinessLogic.Implementation.Blog;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateService;
using AJFIlfov.BusinessLogic.Implementation.Documente;
using AJFIlfov.BusinessLogic.Implementation.EchipeService;
using AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService;
using AJFIlfov.BusinessLogic.Implementation.GrupeService;
using AJFIlfov.BusinessLogic.Implementation.Invoice;
using AJFIlfov.BusinessLogic.Implementation.LocalitatiService;
using AJFIlfov.BusinessLogic.Implementation.MeciLiveService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService;
using AJFIlfov.BusinessLogic.Implementation.StadioaneService;
using AJFIlfov.BusinessLogic.Implementation.StadionLocalitateService;
using AJFIlfov.BusinessLogic.Implementation.TurneeService;
using AJFIlfov.BusinessLogic.Implementation.User;
using AJFIlfov.Code.Base;
using AJFIlfov.Common.DTOs;
using QnA.BusinessLogic;

namespace AJFIlfovWebsite.WebApp.Code.ExtensionMethods;

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
        services.AddScoped<AnuntService>();
        services.AddScoped<MeciLiveService>();
        services.AddScoped<AuditService>();
        services.AddScoped<DocumenteService>();
        services.AddScoped<QuestionService>();
        services.AddScoped<InvoiceService>();
        services.AddScoped<BlogService>();
        services.AddScoped<TurneeService>();
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
            var isParsingSuccessful = Guid.TryParse(userIdClaim, out var id);

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