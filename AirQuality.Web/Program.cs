using AirQuality.Web.Services;
using AirQuality.Web.Services.Interfaces;
using AirQuality.Web.Models.AppSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddOptions();
builder.Services.Configure<OpenAqConfig>(builder.Configuration.GetSection("OpenAq"));
builder.Services.Configure<CachingConfig>(builder.Configuration.GetSection("Caching"));
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddScoped<IOpenAqService, OpenAqService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);

builder.Services.AddStackExchangeRedisCache(options => 
{ 
    options.Configuration = builder.Configuration.GetValue<string>("Caching:RedisCacheUrl"); 
});

var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
