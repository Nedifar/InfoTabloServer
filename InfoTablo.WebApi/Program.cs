using InfoTablo.Application;
using InfoTablo.Application.Common.Mappings;
using InfoTablo.Application.Interfaces;
using InfoTablo.Persistence;
using InfoTablo.WebApi.Services;
using InfoTabloServer.SiganlR;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = AppContext.BaseDirectory + xmlFile;
    config.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssesmblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssesmblyMappingProfile(typeof(IInfoTabloDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddSignalR(opt =>
{
    opt.EnableDetailedErrors = true;
    opt.ClientTimeoutInterval = TimeSpan.FromMinutes(1);
    opt.HandshakeTimeout = TimeSpan.FromSeconds(30);
    opt.KeepAliveInterval = TimeSpan.FromSeconds(30);
});

builder.Services.AddCors(cors =>
{
    cors.AddPolicy("ProductionPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000",
        "http://localhost:3001",
        "http://infotab.okeit.edu",
        "https://infotab.oksei.ru",
        "http://localhost:5014",
        "http://192.168.147.58:86"));
});

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<InfoTabloDbContext>();
        DbInitializer.Initialize(context);
    }
    catch
    {

    }
}

app.UseSwagger();
app.UseSwaggerUI(p =>
{
    p.RoutePrefix = String.Empty;
    p.SwaggerEndpoint("swagger/v1/swagger.json", "Tablo API");
});

app.UseCors("ProductionPolicy");

app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();
    endpoints.MapHub<ScheduleHub>("/ScheduleHub");
    endpoints.MapFallbackToFile("index.html");
});

app.Run();