using CEMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CEMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
        public DbSet<Participant> Participants => Set<Participant>();
        public DbSet<Venue> Venues => Set<Venue>();
        public DbSet<Activity> Activities => Set<Activity>();
        public DbSet<Registration> Registrations => Set<Registration>();
        public DbSet<EventActivity> EventActivities => Set<EventActivity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventActivity>()
                .HasKey(ea => new { ea.EventId, ea.ActivityId });

            modelBuilder.Entity<Registration>()
                .HasIndex(r => new { r.ParticipantId, r.EventId })
                .IsUnique();
        }
    }
}