using DeviceManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Data
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Brand>()
                .HasMany(e => e.Devices)
                .WithOne(e => e.Brand)
                .HasForeignKey(e => e.BrandId)
                .IsRequired();
        }
    }
}
