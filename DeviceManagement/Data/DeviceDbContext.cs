using DeviceManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Data
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
    }
}
