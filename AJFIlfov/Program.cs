using AJFIlfov.BusinessLogic;
using AJFIlfov.DataAccess;
using AJFIlfov.WebApp.Code;
using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.DataAccess;
using AJFIlfov.WebApp.Code;
using Microsoft.EntityFrameworkCore;
using AJFIlfov.WebApp.Code.ExtensionMethods;
using AJFIlfov.WebApp.Code.ExtensionMethods;
using AJFIlfov.WebApp.Code;
using AJFIlfov.DataAccess.EntityFramework;
using AJFIlfov.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Add services to the container.

var connectionString = builder.Configuration["ConnectionString"];
object value = builder.Services.AddDbContext<AjfilfovContext>(options =>
        options.UseSqlServer(connectionString));

//builder.Configure.AddConfiguration((hostingContext, config) =>
//{
//    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
//          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//          .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true)
//          .AddEnvironmentVariables();
//});

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
           options.AccessDeniedPath = new PathString("/Home/Index");
           options.LoginPath = new PathString("/Account/Login");
       });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
});

var twilioSettings = builder.Configuration.GetSection("Twilio");
var accountSid = twilioSettings["AccountSid"];
var authToken = twilioSettings["AuthToken"];
var fromPhoneNumber = twilioSettings["FromPhoneNumber"];

builder.Services.AddSingleton(new SmsService(accountSid, authToken, fromPhoneNumber));

builder.Services.AddAutoMapper(options =>
{
    options.AddMaps(typeof(Program), typeof(BaseService));
});
builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddSignalR();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
