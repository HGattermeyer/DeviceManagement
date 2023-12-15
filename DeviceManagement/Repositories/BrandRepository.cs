using DeviceManagement.Data;
using DeviceManagement.DTOs;
using DeviceManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DeviceManagement.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DeviceDbContext _context;

        public BrandRepository(DeviceDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> CreateBrandAsync(Brand newBrand)
        {
            _context.Brands.Add(newBrand);
            await _context.SaveChangesAsync();
            return newBrand;
        }

        public async Task<Brand> GetBrandByNameAsync(string brandName)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(x => x.Name.ToLower() == brandName.ToLower());
        }

        public async Task<bool> SaveChanges() =>
            await _context.SaveChangesAsync() > 0;
    }
}
