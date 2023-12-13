using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManagement.Entities
{
    [Table("Devices")]
    public class Device
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public Guid BrandId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
