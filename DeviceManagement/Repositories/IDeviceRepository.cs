using DeviceManagement.Entities;

namespace DeviceManagement.Repositories
{
    public interface IDeviceRepository
    {                                                         
        Task<Device> GetDeviceByIdAsync(Guid deviceId);
        Task<Device> GetDeviceByNameAsync(string deviceName);
        Task<List<Device>> GetDevicesByBrandAsync(string brandName);
        Task<List<Device>> GetAllDevicesAsync();
        Task<bool> SaveChanges();
        Task<Device> CreateDeviceAsync(Device device);
        Task<bool> DeleteDeviceAsync(Device device);
        Task<Device> UpdateDeviceAsync(Device device);
    }
}
