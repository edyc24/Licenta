using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.DataAccess;
using AJFIlfov.DataAccess.EntityFramework;
using AJFIlfov.Services;
using AJFIlfov.WebApp.Code;
using AJFIlfov.WebApp.Code.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AjfilfovContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAJFIlfovCurrentUser();
builder.Services.AddPresentation();
builder.Services.AddAJFIlfovBusinessLogic();

builder.Services.AddAuthentication("AJFIlfovCookies")
    .AddCookie("AJFIlfovCookies", options =>
    {
        options.AccessDeniedPath = new PathString("/HomeWebsite/Index");
        options.LoginPath = new PathString("/Admin/Login");
    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews(options => { options.Filters.Add(typeof(GlobalExceptionFilterAttribute)); });

var twilioSettings = builder.Configuration.GetSection("Twilio");
var accountSid = twilioSettings["AccountSid"];
var authToken = twilioSettings["AuthToken"];
var fromPhoneNumber = twilioSettings["FromPhoneNumber"];

builder.Services.AddSingleton(new SmsService(accountSid, authToken, fromPhoneNumber));

builder.Services.AddAutoMapper(options => { options.AddMaps(typeof(Program), typeof(BaseService)); });
builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddSignalR();

var app = builder.Build();

// Middleware to handle subdomain routing
app.UseMiddleware<SubdomainMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeWebsite}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "turnee",
    pattern: "{controller=Turnee}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "arbitrii",
    pattern: "{controller=Arbitrii}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "blog",
    pattern: "{controller=Blog}/{action=Index}/{id?}");


app.Run();

// Middleware class for subdomain handling
public class SubdomainMiddleware
{
    private readonly RequestDelegate _next;

    public SubdomainMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var host = context.Request.Host.Host;

        Console.WriteLine($"Original Path: {context.Request.Path}");

        if (host.StartsWith("turnee.ajfilfov.ro"))
        {
            context.Request.Path = "/Turnee" + context.Request.Path;
        }
        if (host.StartsWith("arbitrii.ajfilfov.ro"))
        {
            context.Request.Path = "/Arbitrii" + context.Request.Path;
        }
        if (host.StartsWith("blog.ajfilfov.ro"))
        {
            context.Request.Path = "/Blog" + context.Request.Path;
        }


        Console.WriteLine($"Modified Path: {context.Request.Path}");



        await _next(context);
    }
}

public static class SubdomainMiddlewareExtensions
{
    public static IApplicationBuilder UseSubdomainMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SubdomainMiddleware>();
    }
}