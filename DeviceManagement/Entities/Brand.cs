using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManagement.Entities
{
    [Table("Brands")]
    public class Brand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Device> Devices { get; set; }
    }
}
