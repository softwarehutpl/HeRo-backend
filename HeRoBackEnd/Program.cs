using Common.Helpers;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using Common.ConfigClasses;
using NLog;
using NLog.Web;
using AutoMapper;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
logger.Debug("Initializing web application");

var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
NLog.GlobalDiagnosticsContext.Set("LogDirectory", logPath);
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = new TimeSpan(0, 20, 0);
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Cookie.HttpOnly = true;
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminRequirment",
            policy => policy.RequireClaim("RoleName", "Admin"));
    });


    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    var config = builder.Configuration.GetSection("CompanyEmailData").Get<EmailConfiguration>();

    builder.Services.AddSingleton(config);
    builder.Services.AddScoped<EmailHelper>();
    builder.Services.AddScoped<EmailService>();
    builder.Services.AddScoped<UserRepository>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<AuthService>();
    builder.Services.AddScoped<RecruitmentRepository>();
    builder.Services.AddScoped<RecruitmentService>();
    builder.Services.AddControllersWithViews();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Version = "v1",
            Title = "API documentation",
            Description = "API documentation for the HeRo app"
        });
        var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    });

    builder.Services.AddAutoMapper(typeof(Program));

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var app = builder.Build();

    app.UseMigrationsEndPoint();
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseDeveloperExceptionPage();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseCookiePolicy(
        new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always
        });

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
