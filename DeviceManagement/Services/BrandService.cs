using DeviceManagement.DTOs;
using DeviceManagement.Entities;
using DeviceManagement.Exceptions;
using DeviceManagement.Repositories;

namespace DeviceManagement.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Brand> CreateBrandAsync(string brandName)
        {
            var brand = await _brandRepository.GetBrandByNameAsync(brandName);

            if (brand != null)
            {
                throw new BrandAlreadyExistsException();
            }

            var newBrand = new Brand()
            {
                Name = brandName,
            };

            await _brandRepository.CreateBrandAsync(newBrand);
            return newBrand;
;
        }

        public async Task<Brand> GetBrandByNameAsync(string brandName)
        {
            return await _brandRepository.GetBrandByNameAsync(brandName);
        }
    }
}
