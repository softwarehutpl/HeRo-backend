using Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Services.Services;
using Hangfire;

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

    builder.Services.AddDistributedMemoryCache();

    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = new TimeSpan(14, 0, 0, 0);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.SlidingExpiration = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.IsEssential = true;
        options.ExpireTimeSpan = new TimeSpan(14, 0, 0, 0);
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Cookie.HttpOnly = false;
    });

    //Hangfire
    builder.Services.AddHangfire(x => x
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString)
        );
    builder.Services.AddHangfireServer();

    builder.Services.AddCors(options => options.AddPolicy("corspolicy", build =>
    {
        build
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .Build();
    }));

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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
    app.UseHangfireDashboard();
    app.UseHangfireServer();
    app.UseRouting();

    RecurringJob.AddOrUpdate<EmailService>(x => x.SaveAllEmailsToDataBase(), Cron.Hourly);

    app.UseSession();

    app.UseCors("corspolicy");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}").RequireCors("corspolicy");

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