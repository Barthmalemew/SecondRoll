using Microsoft.EntityFrameworkCore;
using SecondRoll.Models;
using SecondRoll.Data;
using SecondRoll.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<PlayerContext>(options =>
    options.UseSqlite("Data Source=players.db"));
builder.Services.AddSignalR();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PlayerContext>();
    SeedDatabase(dbContext);
}

void SeedDatabase(PlayerContext context)
{
    // Seed the database with a test user if none exists
    if (!context.Players.Any())
    {
        context.Players.Add(new Player
        {
            Username = "testuser",
            Password = "password123"  // Use plain text for testing only; consider hashing in production
        });
        context.SaveChanges();
    }
}

// Enable serving static files from wwwroot
app.UseStaticFiles();

app.UseRouting();

app.MapHub<ChatHub>("/chatHub");
app.MapControllers();

app.MapFallbackToFile("/login", "login.html");
app.MapFallbackToFile("/chat", "index.html");

app.Run();