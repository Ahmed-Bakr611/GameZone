using GameZone.Models;

namespace GameZone.Data
{
    public class GameDbContext:DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevices> GameDevices{ get; set; }
     
        
        public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options) { }

        //fluent api configrations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevices>().HasKey(e => new { e.GameID, e.DeviceID });
            base.OnModelCreating(modelBuilder);
        }
    }
}
