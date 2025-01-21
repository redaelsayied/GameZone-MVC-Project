
using GameZone.Models;

namespace GameZone.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasKey(g => g.Id);
            modelBuilder.Entity<GameDevice>().HasKey(gd => new { gd.DeviceId, gd.GameId });
            modelBuilder.Entity<Device>().HasKey(d => d.Id);
            modelBuilder.Entity<Category>().HasKey(c => c.Id);

            modelBuilder.Entity<Category>().HasMany(c => c.Games).WithOne(g => g.Category)
                .HasForeignKey(g => g.CategoryID);

            modelBuilder.Entity<Game>().HasMany(g => g.GameDevices).WithOne(gd => gd.Game)
                .HasForeignKey(gd => gd.GameId);

            modelBuilder.Entity<GameDevice>().HasOne(gd => gd.Device).WithMany(d => d.GameDevices)
                .HasForeignKey(d => d.DeviceId);

            modelBuilder.Entity<Game>().Property(p => p.Name).HasMaxLength(250);
            modelBuilder.Entity<Game>().Property(p => p.Description).HasMaxLength(2500);
            modelBuilder.Entity<Game>().Property(p => p.Cover).HasMaxLength(500);

            modelBuilder.Entity<Category>().Property(p => p.Name).HasMaxLength(250);

            modelBuilder.Entity<Device>().Property(p => p.Name).HasMaxLength(250);
            modelBuilder.Entity<Device>().Property(p => p.Icon).HasMaxLength(100);

        }
    }
}
