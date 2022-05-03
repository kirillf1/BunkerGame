using BunkerGame.Application;
using BunkerGame.Infrastructure;
using BunkerGame.Infrastructure.Database;
using BunkerGame.Infrastructure.JsonContext;
using BunkerGame.VkApi;
using BunkerGame.VkApi.NotificationHandlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

bool inMemory = true;
builder.Services.AddInfrastructure(builder.Configuration, inMemory);
builder.Services.AddApplication();
builder.Services.AddVkServices(builder.Configuration);
var assemblyApplication = typeof(ApplicationAssembly).GetTypeInfo().Assembly;
var currentAssembly = typeof(GameEndedNotificationHandler).GetTypeInfo().Assembly;
builder.Services.AddMediatR(assemblyApplication, currentAssembly);
builder.Services.AddControllers().AddNewtonsoftJson();
if (!builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.Listen(IPAddress.Any, Convert.ToInt32(Environment.GetEnvironmentVariable("PORT")));
    });
}
var app = builder.Build();
if (inMemory)
    await SeedFromJson(app.Services);
else
    await EnsureDbAsync(app.Services);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
static async Task SeedFromJson(IServiceProvider sp)
{
    var path = Path.Combine(Directory.GetCurrentDirectory(), "bunkerGame.json");
    var jsonContext = JsonSerializer.Deserialize<BunkerGameJsonContext>(File.ReadAllText(path))!;
    await using var db = sp.CreateScope().ServiceProvider.GetRequiredService<BunkerGameDbContext>();
    await db.AddRangeAsync(jsonContext.AdditionalInformations);
    await db.AddRangeAsync(jsonContext.BunkerEnviroments);
    await db.AddRangeAsync(jsonContext.BunkerObjects);
    await db.AddRangeAsync(jsonContext.BunkerWalls);
    await db.AddRangeAsync(jsonContext.Cards);
    await db.AddRangeAsync(jsonContext.Catastrophes);
    await db.AddRangeAsync(jsonContext.CharacterItems);
    await db.AddRangeAsync(jsonContext.ExternalSurroundings);
    await db.AddRangeAsync(jsonContext.ItemBunkers);
    await db.AddRangeAsync(jsonContext.Healths);
    await db.AddRangeAsync(jsonContext.Hobbies);
    await db.AddRangeAsync(jsonContext.Phobias);
    await db.AddRangeAsync(jsonContext.Professions);
    await db.AddRangeAsync(jsonContext.Traits);
    await db.SaveChangesAsync();
}

static async Task EnsureDbAsync(IServiceProvider sp)
{
    await using var db = sp.CreateScope().ServiceProvider.GetRequiredService<BunkerGameDbContext>();
    await db.Database.MigrateAsync();
}