using DeviceManagement.DTOs;
using DeviceManagement.Entities;

namespace DeviceManagement.Services
{
    public interface IBrandService
    {
        Task<Brand> GetBrandByNameAsync(string brandName);
        Task<Brand> CreateBrandAsync(string brandName);
    }
}
