using DeviceManagement.DTOs;
using DeviceManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.Services
{
    public interface IDeviceService
    {
        Task<List<Device>> GetAllDevicesAsync();
        Task<Device> GetDeviceByIdAsync(Guid deviceId);
        Task<Device> CreateDevice(CreateDeviceDto deviceDto);
        Task<bool> Delete(Guid id);
        Task<Device> UpdateDevice(Guid deviceId, UpdateDeviceDto updateDeviceDto);
        Task<List<Device>> GetDevicesByBrandName(string brandName);
    }
}
