using InfoTabloServer.BackgroundServices;
using InfoTabloServer.BackgroundServices.SheduleHostedServices;
using InfoTabloServer.Context;
using InfoTabloServer.SiganlR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = "background"
});

string connectionString = "Host=192.168.147.58; port=5432; DataBase=TabloRecreate; username=postgres; password=1";

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000", 
        "http://localhost:3001", 
        "http://infotab.okeit.edu", 
        "https://infotab.oksei.ru", 
        "http://localhost:5014",
        "http://127.0.0.1:5500",
        "http://192.168.147.58:86"));
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<context>(options => options.UseNpgsql(connectionString).UseLazyLoadingProxies());
builder.Services.AddControllers();

builder.Services.AddSignalR(p =>
{
    p.EnableDetailedErrors = true;
    p.ClientTimeoutInterval = TimeSpan.FromMinutes(1);
    p.HandshakeTimeout = TimeSpan.FromSeconds(30);
    p.KeepAliveInterval = TimeSpan.FromSeconds(30);
}
);

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddHostedService<MainSheduleHostedService>();
builder.Services.AddHostedService<BackgroundImageBackService>();
builder.Services.AddHostedService<NewSheduleHostedService>();
builder.Services.AddHostedService<FloorSheduleHostedService>();
builder.Services.AddHostedService<WeekNameHostedService>();
builder.Services.AddHostedService<AdminHostedSerivce>();
builder.Services.AddHostedService<AnnouncmentHostedService>();
builder.Services.AddHostedService<FullUpdateHostedService>();
builder.Services.AddHostedService<DayPartHeadersService>();
builder.Services.AddHostedService<WeatherHostedService>();

builder.Services.AddMemoryCache();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseResponseCompression();
app.UseDeveloperExceptionPage();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<GoodHubGbl>("/GoodHubGbl");
    endpoints.MapFallbackToFile("index.html");
});

app.Run();
