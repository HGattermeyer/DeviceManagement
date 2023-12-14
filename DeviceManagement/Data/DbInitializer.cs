using DeviceManagement.Entities;
using Microsoft.EntityFrameworkCore;


namespace DeviceManagement.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            SeedData(scope.ServiceProvider.GetService<DeviceDbContext>());
        }



        private static void SeedData(DeviceDbContext context)
        {
            context.Database.Migrate();

            if (context.Devices.Any())
            {
                Console.WriteLine("Already have data - no need to seed again.");
                return;
            }

            var devices = new List<Device>() 
            {
                new Device()
                {
                    Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                    Brand = new Brand()
                    {
                        Name = "Apple"
                    },
                    Name = "iPhone",
                }
            };

            context.AddRange(devices);

            context.SaveChanges();
        }
    }
}
