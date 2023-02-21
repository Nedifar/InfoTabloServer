using InfoTabloServer.BackgroundServices;
using InfoTabloServer.BackgroundServices.SheduleHostedServices;
using InfoTabloServer.Context;
using InfoTabloServer.SiganlR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Host=192.168.147.69; port=5432; DataBase=InformationTabloBase; username=postgres; password=nw6Gs79d";

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000", "http://infotab.okeit.edu", "https://infotab.oksei.ru", "http://localhost:5014"));
});

builder.Environment.WebRootPath = "background";
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

app.UseCors("CorsPolicy");
app.UseResponseCompression();
app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<GoodHubGbl>("/GoodHubGbl");
    endpoints.MapFallbackToFile("index.html");
});

app.Run();
