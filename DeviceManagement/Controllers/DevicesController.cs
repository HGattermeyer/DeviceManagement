using AutoMapper;
using AutoMapper.QueryableExtensions;
using DeviceManagement.Data;
using DeviceManagement.DTOs;
using DeviceManagement.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceDbContext _context;
        private readonly IMapper _mapper;

        public DevicesController(DeviceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeviceDto>>> GetAllDevices()
        {
            var query = _context.Devices.OrderBy(x => x.CreatedDate);

            return await query.ProjectTo<DeviceDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDto>> GetAllDevices(Guid? id)
        {
            var device = _context.Devices
                .Include(x => x.Brand)
                .FirstOrDefault(d => d.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return _mapper.Map<DeviceDto>(device);
        }

        [HttpPost]
        public async Task<ActionResult<DeviceDto>> CreateDevice(CreateDeviceDto deviceDto)
        {
            var device = _mapper.Map<Device>(deviceDto);

            var isDeviceExists = _context.Devices.FirstOrDefault(d => d.Name == device.Name);

            if(isDeviceExists != null) 
            {
                return BadRequest("Device already exists");
            }

            Console.WriteLine($"Device brand name: {device.Brand.Name}");
            var brand = _context.Brands.FirstOrDefault(d => d.Name == device.Brand.Name);

            if (brand != null)
            {
                device.Brand = brand;
            }

            _context.Add(device);

            var newDevice = _mapper.Map<DeviceDto>(device);

            var result = await _context.SaveChangesAsync() > 0;

            if(!result)
            {
                return BadRequest("Could not save changes to DB.");
            }

            return CreatedAtAction(nameof(CreateDevice), newDevice);
        }

        [HttpDelete("{id}")]
        //TODO put id in route
        public async Task<ActionResult<DeviceDto>> Delete(Guid id)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id);
            if(device != null)
            {
                _context.Devices.Remove(device); 
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Device not found");
        }

        [HttpPut("{deviceId}")]
        public async Task<ActionResult<DeviceDto>> UpdateDevice(Guid deviceId, UpdateDeviceDto updateDeviceDto)
        {
            var device = _context.Devices.
                Include(x => x.Brand).
                FirstOrDefault(d => d.Id == deviceId);

            if (device == null)
            {
                return BadRequest("Device not found");
            }

            if(!string.IsNullOrEmpty(updateDeviceDto.BrandName))
            {
                var brand = await _context.Brands
                    .FirstOrDefaultAsync(x => x.Name == updateDeviceDto.BrandName);

                if(brand != null)
                {
                    device.Brand = brand;
                }
                else
                {
                    device.Brand = new Brand()
                    {
                        Name = updateDeviceDto.BrandName,
                    };
                }
            }

            device.Name = updateDeviceDto.Name ?? device.Name;

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return BadRequest("Problim saving changes.");
            }

            return Ok(device);
        }

    }
}
