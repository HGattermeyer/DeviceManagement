using AutoMapper;
using DeviceManagement.DTOs;
using DeviceManagement.Entities;

namespace DeviceManagement.RequestHelper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Device, DeviceDTO>().IncludeMembers(x => x.Brand);
            CreateMap<Brand, DeviceDTO>();
            CreateMap<CreateDeviceDTO, Device>().ForMember(x => x.Brand, o => o.MapFrom(s => 2));
            CreateMap<CreateDeviceDTO, Brand>();
        }
    }
}
