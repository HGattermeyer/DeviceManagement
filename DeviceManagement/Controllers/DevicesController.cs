using AutoMapper;
using AutoMapper.QueryableExtensions;
using DeviceManagement.Data;
using DeviceManagement.DTOs;
using DeviceManagement.Entities;
using DeviceManagement.Exceptions;
using DeviceManagement.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _DeviceService;
        private readonly IMapper _mapper;

        public DevicesController(IDeviceService deviceService, IMapper mapper)
        {
            _DeviceService = deviceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeviceDto>>> GetAllDevices()
        {
            try
            {
                var allDevices = await _DeviceService.GetAllDevicesAsync();

                return _mapper.Map<List<DeviceDto>>(allDevices);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDto>> GetDeviceById(Guid id)
        {
            try
            {
                var device = await _DeviceService.GetDeviceByIdAsync(id);
                return _mapper.Map<DeviceDto>(device);
            }
            catch (DeviceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DeviceDto>> CreateDevice(CreateDeviceDto deviceDto)
        {
            try
            {
                var newDevice = await _DeviceService.CreateDevice(deviceDto);
                Console.WriteLine("4");

                return _mapper.Map<DeviceDto>(newDevice);

            }
            catch (DeviceAlreadyExistsException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        //TODO put id in route
        public async Task<ActionResult<DeviceDto>> Delete(Guid id)
        {
            try
            {
                var isDeleted = await _DeviceService.Delete(id);
                if (isDeleted)
                {
                    return Ok();
                }

                return BadRequest("Error deleting");

            }
            catch (DeviceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{deviceId}")]
        public async Task<ActionResult<DeviceDto>> UpdateDevice(Guid deviceId, UpdateDeviceDto updateDeviceDto)
        {
            try
            {

                var device = await _DeviceService.UpdateDevice(deviceId, updateDeviceDto);
                return _mapper.Map<DeviceDto>(device);
            }
            catch (DeviceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("brand/{brandName}")]
        public async Task<ActionResult<List<DeviceDto>>> GetDevicesByBrandName(string brandName)
        {
            try
            {
                var devicesByBrand = await _DeviceService.GetDevicesByBrandName(brandName);
                return _mapper.Map<List<DeviceDto>>(devicesByBrand);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
