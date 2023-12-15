using AutoMapper;
using DeviceManagement.DTOs;
using DeviceManagement.Entities;
using DeviceManagement.Exceptions;
using DeviceManagement.Repositories;

namespace DeviceManagement.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IBrandService _brandService;

        public DeviceService(IDeviceRepository deviceRepository, IBrandRepository brandRepository, IBrandService brandService)
        {
            _deviceRepository = deviceRepository;
            _brandService = brandService;
            _brandRepository = brandRepository;
        }

        public async Task<Device> CreateDevice(CreateDeviceDto deviceDto)
        {
            var existingDevice = await _deviceRepository.GetDeviceByNameAsync(deviceDto.Name);

            if (existingDevice != null)
            {
                throw new DeviceAlreadyExistsException();
            }

            var existingBrand = await _brandRepository.GetBrandByNameAsync(deviceDto?.BrandName);
            if (existingBrand == null)
            {
                var newBrand = new Brand()
                {
                    Name = deviceDto.BrandName,
                };
                existingBrand = await _brandRepository.CreateBrandAsync(newBrand);
            }

            var device = new Device
            {
                Name = deviceDto.Name,
                Brand = existingBrand,
            };

            return await _deviceRepository.CreateDeviceAsync(device);
        }

        public async Task<bool> Delete(Guid id)
        {
            var device = await _deviceRepository.GetDeviceByIdAsync(id);
            if(device == null)
            {
                throw new DeviceNotFoundException("Device not found.");
            }

            return await _deviceRepository.DeleteDeviceAsync(device);
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            return await _deviceRepository.GetAllDevicesAsync();
        }

        public async Task<Device> GetDeviceByIdAsync(Guid deviceId)
        {
            var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);

            if(device == null )
            {
                throw new DeviceNotFoundException();
            }

            return device;
        }

        public async Task<List<Device>> GetDevicesByBrandName(string brandName)
        {
            return await _deviceRepository.GetDevicesByBrandAsync(brandName);
        }

        public async Task<Device> UpdateDevice(Guid deviceId, UpdateDeviceDto updateDeviceDto)
        {
            var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);
            if (device == null)
            {
                throw new DeviceNotFoundException();
            }

            await AssignExistingOrCreateNewBrand(updateDeviceDto, device);

            device.Name = updateDeviceDto.Name ?? device.Name;

            await _deviceRepository.UpdateDeviceAsync(device);

            return device;
        }

        private async Task AssignExistingOrCreateNewBrand(UpdateDeviceDto updateDeviceDto, Device device)
        {
            var existingBrand = await _brandService.GetBrandByNameAsync(updateDeviceDto.BrandName);
            if (existingBrand != null)
            {
                device.Brand = existingBrand;
            }
            else
            {
                var newBrand = await _brandService.CreateBrandAsync(updateDeviceDto.BrandName);
                device.Brand = newBrand;
            }
        }
    }
}
