using AutoMapper;
using DeviceManagement.DTOs;
using DeviceManagement.Entities;

namespace DeviceManagement.RequestHelper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Device, DeviceDto>().IncludeMembers(x => x.Brand);
            CreateMap<Brand, DeviceDto>();
            CreateMap<Device, CreateDeviceDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

            CreateMap<CreateDeviceDto, Device>()
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForPath(dest => dest.Brand.Name, opt => opt.MapFrom(src => src.BrandName))
                        .ForMember(dest => dest.BrandId, opt => opt.Ignore());
        }
    }
}
