using CEMS.Components;
using CEMS.Data;
using CEMS.Models;
using CEMS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=cems.db"));
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddSingleton<SessionService>();
var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Venues.Any())
    {
        db.Venues.AddRange(
            new Venue { Name = "City Hall", Address = "1 Main St", Capacity = 200 },
            new Venue { Name = "Community Centre", Address = "45 Park Rd", Capacity = 100 },
            new Venue { Name = "Library Hall", Address = "12 Book Lane", Capacity = 50 }
        );

        db.Activities.AddRange(
            new Activity { Name = "Morning Workshop", Type = "Workshop" },
            new Activity { Name = "Keynote Talk", Type = "Talk" },
            new Activity { Name = "Team Games", Type = "Game" },
            new Activity { Name = "Networking Session", Type = "Workshop" }
        );

        db.SaveChanges();

        db.Events.AddRange(
            new Event
            {
                Name = "Spring Community Day",
                Date = DateTime.Today.AddDays(7),
                Time = new TimeSpan(10, 0, 0),
                Description = "A fun day for the whole community.",
                VenueId = 1,
                EventActivities = new List<EventActivity>
                {
                    new EventActivity { ActivityId = 1 },
                    new EventActivity { ActivityId = 3 }
                }
            },
            new Event
            {
                Name = "Tech Talk Evening",
                Date = DateTime.Today.AddDays(14),
                Time = new TimeSpan(18, 30, 0),
                Description = "Industry speakers on emerging technology.",
                VenueId = 2,
                EventActivities = new List<EventActivity>
                {
                    new EventActivity { ActivityId = 2 },
                    new EventActivity { ActivityId = 4 }
                }
            }
        );

        db.Participants.AddRange(
            new Participant { Name = "Alice Johnson", Email = "alice@email.com", Phone = "07700100001" },
            new Participant { Name = "Bob Smith", Email = "bob@email.com", Phone = "07700100002" }
        );

        db.SaveChanges();
    }
}

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
