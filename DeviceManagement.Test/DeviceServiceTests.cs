using Moq;
using DeviceManagement.Entities;
using DeviceManagement.Repositories;
using DeviceManagement.Services;
using DeviceManagement.Exceptions;
using DeviceManagement.DTOs;

[TestFixture]
public class DeviceServiceTests
{
    [Test]
    public async Task GetAllDevicesAsync_ShouldReturnListOfDevices_WhenRepositoryHasData()
    {
        // Arrange
        var expectedDevices = new List<Device>
        {
            new Device { Id = Guid.NewGuid(), Name = "Device 1" },
            new Device { Id = Guid.NewGuid(), Name = "Device 2" }
            // Add more devices if needed
        };

        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetAllDevicesAsync()).ReturnsAsync(expectedDevices);

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act
        var result = await deviceService.GetAllDevicesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(expectedDevices.Count, result.Count);
        // Add more assertions to check individual device properties, etc.
    }

    [Test]
    public async Task GetAllDevicesAsync_ShouldReturnEmptyList_WhenRepositoryHasNoData()
    {
        // Arrange
        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetAllDevicesAsync()).ReturnsAsync(new List<Device>());

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act
        var result = await deviceService.GetAllDevicesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result);
    }

    [Test]
    public async Task Delete_ShouldReturnTrue_WhenDeviceExistsAndIsDeleted()
    {
        // Arrange
        var deviceId = Guid.NewGuid();
        var mockDeviceRepository = new Mock<IDeviceRepository>();

        var existingDevice = new Device { Id = deviceId, Name = "Test Device" };
        mockDeviceRepository.Setup(repo => repo.GetDeviceByIdAsync(deviceId)).ReturnsAsync(existingDevice);
        mockDeviceRepository.Setup(repo => repo.DeleteDeviceAsync(existingDevice)).ReturnsAsync(true);

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act
        var result = await deviceService.Delete(deviceId);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Delete_ShouldThrowDeviceNotFoundException_WhenDeviceDoesNotExist()
    {
        // Arrange
        var deviceId = Guid.NewGuid();
        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDeviceByIdAsync(deviceId)).ReturnsAsync((Device)null);

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act and Assert
        Assert.ThrowsAsync<DeviceNotFoundException>(async () => await deviceService.Delete(deviceId));
    }

    [Test]
    public async Task CreateDevice_ShouldThrowDeviceAlreadyExistsException_WhenDeviceWithSameNameExists()
    {
        // Arrange
        var deviceDto = new CreateDeviceDto { Name = "ExistingDevice", BrandName = "ExistingBrand" };
        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDeviceByNameAsync(deviceDto.Name)).ReturnsAsync(new Device());

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act and Assert
        Assert.ThrowsAsync<DeviceAlreadyExistsException>(async () => await deviceService.CreateDevice(deviceDto));
    }

    [Test]
    public async Task CreateDevice_ShouldCreateDevice_WhenBrandDoesNotExist()
    {
        // Arrange
        var deviceDto = new CreateDeviceDto { Name = "NewDevice", BrandName = "NewBrand" };
        var brand = new Brand() { Name = "NewBrand", Id = Guid.NewGuid() };
        var device = new Device() { Brand =  brand, Id = Guid.NewGuid(), Name = "NewDevice" };

        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDeviceByNameAsync(deviceDto.Name)).ReturnsAsync((Device)null);
        mockDeviceRepository.Setup(repo => repo.CreateDeviceAsync(device)).ReturnsAsync(device);

        var mockBrandRepository = new Mock<IBrandRepository>();
        mockBrandRepository.Setup(repo => repo.GetBrandByNameAsync(deviceDto.BrandName)).ReturnsAsync((Brand)null);
        mockBrandRepository.Setup(repo => repo.CreateBrandAsync(It.IsAny<Brand>())).ReturnsAsync(brand);

        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act
        var result = await deviceService.CreateDevice(deviceDto);

        // Assert
        Assert.NotNull(result);
    }

    [Test]
    public async Task CreateDevice_ShouldCreateDevice_WhenBrandExists()
    {
        // Arrange
        var deviceDto = new CreateDeviceDto { Name = "NewDevice", BrandName = "ExistingBrand" };
        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDeviceByNameAsync(deviceDto.Name)).ReturnsAsync((Device)null);

        var existingBrand = new Brand { Name = "ExistingBrand" };
        var mockBrandRepository = new Mock<IBrandRepository>();
        mockBrandRepository.Setup(repo => repo.GetBrandByNameAsync(deviceDto.BrandName)).ReturnsAsync(existingBrand);

        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act
        var result = await deviceService.CreateDevice(deviceDto);

        // Assert
        Assert.NotNull(result);
    }

    [Test]
    public async Task GetDevicesByBrandName_ShouldReturnDevices_WhenBrandExists()
    {
        // Arrange
        var brandName = "ExistingBrand";
        var devices = new List<Device>
        {
            new Device { Id = Guid.NewGuid(), Name = "Device 1", Brand = new Brand { Name = brandName } },
            new Device { Id = Guid.NewGuid(), Name = "Device 2", Brand = new Brand { Name = brandName } }
            // Add more devices if needed
        };

        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDevicesByBrandAsync(brandName)).ReturnsAsync(devices);

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act
        var result = await deviceService.GetDevicesByBrandName(brandName);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(devices.Count, result.Count);
        // Add more assertions to check individual device properties, etc.
    }

    [Test]
    public async Task GetDevicesByBrandName_ShouldReturnEmptyList_WhenBrandDoesNotExist()
    {
        // Arrange
        var brandName = "NonExistingBrand";
        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDevicesByBrandAsync(brandName)).ReturnsAsync(new List<Device>());

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act
        var result = await deviceService.GetDevicesByBrandName(brandName);

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result);
    }


    [Test]
    public async Task UpdateDevice_ShouldUpdateDevice_WhenDeviceExists()
    {
        // Arrange
        var deviceId = Guid.NewGuid();
        var updateDto = new UpdateDeviceDto { Name = "UpdatedDevice", BrandName = "ExistingBrand" };

        var existingDevice = new Device { Id = deviceId, Name = "ExistingDevice", Brand = new Brand { Name = "ExistingBrand" } };

        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDeviceByIdAsync(deviceId)).ReturnsAsync(existingDevice);
        mockDeviceRepository.Setup(repo => repo.UpdateDeviceAsync(It.IsAny<Device>())).ReturnsAsync(existingDevice);

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();
        mockBrandService.Setup(service => service.GetBrandByNameAsync(updateDto.BrandName)).ReturnsAsync(existingDevice.Brand);

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act
        var result = await deviceService.UpdateDevice(deviceId, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(updateDto.Name, result.Name);
    }

    [Test]
    public async Task UpdateDevice_ShouldThrowDeviceNotFoundException_WhenDeviceDoesNotExist()
    {
        // Arrange
        var deviceId = Guid.NewGuid();
        var updateDto = new UpdateDeviceDto { Name = "UpdatedDevice", BrandName = "ExistingBrand" };

        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDeviceByIdAsync(deviceId)).ReturnsAsync((Device)null);

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act and Assert
        Assert.ThrowsAsync<DeviceNotFoundException>(async () => await deviceService.UpdateDevice(deviceId, updateDto));
    }

    [Test]
    public async Task GetDeviceByIdAsync_ShouldReturnDevice_WhenDeviceExists()
    {
        // Arrange
        var deviceId = Guid.NewGuid();
        var existingDevice = new Device { Id = deviceId, Name = "TestDevice" };

        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDeviceByIdAsync(deviceId)).ReturnsAsync(existingDevice);

        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);

        // Act
        var result = await deviceService.GetDeviceByIdAsync(deviceId);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(deviceId, result.Id);
        // Add more assertions as needed to check other properties
    }

    [Test]
    public void GetDeviceByIdAsync_ShouldThrowDeviceNotFoundException_WhenDeviceDoesNotExist()
    {
        // Arrange
        var deviceId = Guid.NewGuid();

        var mockDeviceRepository = new Mock<IDeviceRepository>();
        mockDeviceRepository.Setup(repo => repo.GetDeviceByIdAsync(deviceId)).ReturnsAsync((Device)null);


        var mockBrandRepository = new Mock<IBrandRepository>();
        var mockBrandService = new Mock<IBrandService>();

        var deviceService = new DeviceService(mockDeviceRepository.Object, mockBrandRepository.Object, mockBrandService.Object);


        // Act and Assert
        Assert.ThrowsAsync<DeviceNotFoundException>(async () => await deviceService.GetDeviceByIdAsync(deviceId));
    }
}