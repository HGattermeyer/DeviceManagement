using DeviceManagement.Entities;

namespace DeviceManagement.Repositories
{
    public interface IBrandRepository
    {
        Task<Brand> CreateBrandAsync(Brand newBrand);
        Task<Brand> GetBrandByNameAsync(string brandName);
        Task<bool> SaveChanges();

    }
}
