using Common.ConfigClasses;
using Common.Helpers;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using AutoMapper;
using Services.Services;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
logger.Debug("Initializing web application");

var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
NLog.GlobalDiagnosticsContext.Set("LogDirectory", logPath);
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add configurations
    builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = hostingContext.HostingEnvironment;

        config.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //load base settings
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) //load local settings
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) //load environment settings
                .AddEnvironmentVariables();
    });

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.SlidingExpiration = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.Domain = "localhost";
        options.ExpireTimeSpan = new TimeSpan(0, 20, 0);
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Cookie.HttpOnly = true;
    });

    builder.Services.AddCors(options => options.AddPolicy("corspolicy", build =>
    {
        build
        .WithOrigins("http://localhost:3000", "http://localhost:7210")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    }));

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminRequirment",
            policy => policy
            .RequireClaim("RoleName", "ADMIN")
            .RequireClaim("UserStatus", "ACTIVE"));

        options.AddPolicy("HrManagerRequirment",
            policy => policy
            .RequireClaim("RoleName", "HR_MANAGER")
            .RequireClaim("UserStatus", "ACTIVE"));

        options.AddPolicy("RecruiterRequirment",
            policy => policy
            .RequireClaim("RoleName", "RECRUITER")
            .RequireClaim("UserStatus", "ACTIVE"));

        options.AddPolicy("TechnicianRequirment",
            policy => policy
            .RequireClaim("RoleName", "TECHNICIAN")
            .RequireClaim("UserStatus", "ACTIVE"));

        options.AddPolicy("AnyRoleRequirment",
            policy => policy
            .RequireClaim("RoleName", "INTERVIEWER", "RECRUITER", "HR_MANAGER", "ADMIN")
            .RequireClaim("UserStatus", "ACTIVE"));

        options.AddPolicy("HrRequirment",
            policy => policy
            .RequireClaim("RoleName", "RECRUITER", "HR_MANAGER", "ADMIN")
            .RequireClaim("UserStatus", "ACTIVE"));
    });

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    var config = builder.Configuration.GetSection("CompanyEmailData").Get<EmailConfiguration>();

    builder.Services.AddSingleton(config);

    builder.Services.RegisterServices(builder.Configuration);

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
    builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

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

    app.UseCors("corspolicy");

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