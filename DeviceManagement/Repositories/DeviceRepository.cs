using DeviceManagement.Data;
using DeviceManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceDbContext _context;

        public DeviceRepository(DeviceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Device>> GetAllDevicesAsync() => 
            await _context.Devices
            .Include(x => x.Brand)
            .ToListAsync();

        public async Task<List<Device>> GetDevicesByBrandAsync(string brandName) => 
            await _context.Devices
                .Include(x => x.Brand)
                .Where(x => x.Brand.Name.ToLower() == brandName.ToLower())
                .ToListAsync();

        public async Task<Device> GetDeviceByIdAsync(Guid deviceId) => 
            await _context.Devices
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Id == deviceId);

        public async Task<bool> SaveChanges() =>
            await _context.SaveChangesAsync() > 0;

        public async Task<Device> GetDeviceByNameAsync(string deviceName) => 
            await _context.Devices
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Name == deviceName);

        public async Task<Device> CreateDeviceAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return device;
        }

        public async Task<bool> DeleteDeviceAsync(Device device)
        {
            _context.Devices.Remove(device);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Device> UpdateDeviceAsync(Device device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();

            return device;

        }
    }
}
