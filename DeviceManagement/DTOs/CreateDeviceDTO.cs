using DeviceManagement.Entities;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DTOs
{
    public class CreateDeviceDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string BrandName { get; set; }

    }
}
