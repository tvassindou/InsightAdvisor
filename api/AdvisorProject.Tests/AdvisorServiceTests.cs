using AdvisorProject.Application.DTOs;
using AdvisorProject.Application.Services;
using AdvisorProject.Core.Entities;
using AdvisorProject.Core.Interfaces;
using AutoMapper;
using Moq;

namespace AdvisorProject.Tests;
public class AdvisorServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AdvisorService _advisorService;

    public AdvisorServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _advisorService = new AdvisorService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAdvisorByIdAsync_ReturnsAdvisorDto_WhenAdvisorExists()
    {
        // Arrange
        var advisor = new Advisor { Id = 1, FullName = "John Doe", SIN = "123456789" };
        _unitOfWorkMock.Setup(uow => uow.Advisors.GetByIdAsync(1)).ReturnsAsync(advisor);
        _mapperMock.Setup(m => m.Map<AdvisorDto>(advisor)).Returns(new AdvisorDto { Id = 1, FullName = "John Doe" });

        // Act
        var result = await _advisorService.GetAdvisorByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John Doe", result.FullName);
    }

    [Fact]
    public async Task GetPagedAdvisorsAsync_ReturnsPagedResult_WithFilter()
    {
        // Arrange
        var advisors = new List<Advisor>
        {
            new() { Id = 1, FullName = "John Doe", SIN= "987654321" },
            new () { Id = 2, FullName = "Jane Doe", SIN = "123456789" }
        }.AsQueryable();

        _unitOfWorkMock.Setup(uow => uow.Advisors.GetAll()).Returns(advisors);
        _mapperMock.Setup(m => m.Map<IEnumerable<AdvisorDto>>(It.IsAny<IEnumerable<Advisor>>()))
            .Returns(advisors.Select(a => new AdvisorDto { Id = a.Id, FullName = a.FullName }));

        // Act
        var result = await _advisorService.GetPagedAdvisorsAsync(1, 10, "Doe");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalItems);
        Assert.Equal(1, result.PageNumber);
        Assert.NotNull(result.Items);
        Assert.Contains(result.Items, a => !string.IsNullOrEmpty(a.FullName) && a.FullName.Contains("Doe"));
    }

    [Fact]
    public async Task UpdateAdvisorAsync_ReturnsTrue_WhenAdvisorExists()
    {
        // Arrange
        var advisor = new Advisor { Id = 1, FullName = "John Doe", SIN = "987654321" };
        var updateDto = new UpdateAdvisorDto { FullName = "John Smith", Address = "My Address" };

        _unitOfWorkMock.Setup(uow => uow.Advisors.GetByIdAsync(1)).ReturnsAsync(advisor);
        _mapperMock.Setup(m => m.Map(updateDto, advisor)).Callback(() => advisor.FullName = updateDto.FullName);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var result = await _advisorService.UpdateAdvisorAsync(1, updateDto);

        // Assert
        Assert.True(result);
        Assert.Equal("John Smith", advisor.FullName);
    }

    [Fact]
    public async Task DeleteAdvisorAsync_ReturnsTrue_WhenAdvisorExists()
    {
        // Arrange
        var advisor = new Advisor { Id = 1, FullName = "John Doe", SIN = "987654321" };
        _unitOfWorkMock.Setup(uow => uow.Advisors.GetByIdAsync(1)).ReturnsAsync(advisor);
        _unitOfWorkMock.Setup(uow => uow.Advisors.Delete(advisor));
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var result = await _advisorService.DeleteAdvisorAsync(1);

        // Assert
        Assert.True(result);
    }
}
