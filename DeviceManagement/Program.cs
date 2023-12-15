using DeviceManagement.Data;
using DeviceManagement.Repositories;
using DeviceManagement.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DeviceDbContext>(opt =>
{
    Console.WriteLine($"CS: {builder.Configuration.GetConnectionString("DefaultConnection")}");
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IBrandService, BrandService>();

var app = builder.Build();


app.UseAuthorization();

app.MapControllers();

try
{
    DbInitializer.InitDb(app);
} catch(Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();
