using AutoMapper;
using DeviceManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.Controllers
{
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceDbContext _context;
        private readonly IMapper _mapper;

        public DevicesController(DeviceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
