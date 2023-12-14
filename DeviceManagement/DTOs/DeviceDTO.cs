using DeviceManagement.Entities;

namespace DeviceManagement.DTOs
{
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
