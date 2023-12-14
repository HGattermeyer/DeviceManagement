using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DTOs
{
    public class UpdateDeviceDto
    {
        public string? Name { get; set; }
        public string? BrandName { get; set; }
    }
}
