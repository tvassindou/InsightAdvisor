using AdvisorProject.Application.DTOs;
using AdvisorProject.Application.Interfaces;
using AdvisorProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdvisorProject.Tests;

public class AdvisorsControllerTests
{
    private readonly Mock<IAdvisorService> _advisorServiceMock;
    private readonly Mock<ILogger<AdvisorsController>> _loggerMock;
    private readonly AdvisorsController _controller;

    public AdvisorsControllerTests()
    {
        _advisorServiceMock = new Mock<IAdvisorService>();
        _loggerMock = new Mock<ILogger<AdvisorsController>>();
        _controller = new AdvisorsController(_advisorServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAdvisorById_ReturnsOkResult_WhenAdvisorExists()
    {
        // Arrange
        var advisorDto = new AdvisorDto { Id = 1, FullName = "John Doe" };
        _advisorServiceMock.Setup(s => s.GetAdvisorByIdAsync(1)).ReturnsAsync(advisorDto);

        // Act
        var result = await _controller.GetAdvisorById(1);

        // Assert
        var okResult = Assert.IsType<ActionResult<AdvisorDto>>(result);
        Assert.IsType<OkObjectResult>(okResult.Result);

        // Assert
        var objectResult = okResult.Result as OkObjectResult;

        Assert.NotNull(objectResult);
        Assert.Equal(200, objectResult.StatusCode);
        Assert.Equal(advisorDto, objectResult.Value);
    }

    [Fact]
    public async Task GetAdvisorById_ReturnsNotFound_WhenAdvisorDoesNotExist()
    {
        // Arrange
        _advisorServiceMock.Setup(s => s.GetAdvisorByIdAsync(1)).ReturnsAsync(new AdvisorDto());

        // Act
        var result = await _controller.GetAdvisorById(1);

        // Assert
        var notFoundResult = Assert.IsType<ActionResult<AdvisorDto>>(result);
        Assert.IsType<NotFoundResult>(notFoundResult.Result);
    }

    [Fact]
    public async Task GetAllAdvisors_ReturnsOkResult_WithPagedAdvisors()
    {
        // Arrange
        var pagedResult = new PagedResult<AdvisorDto>
        {
            Items = new List<AdvisorDto>
            {
                new AdvisorDto { Id = 1, FullName = "John Doe" },
                new AdvisorDto { Id = 2, FullName = "Jane Doe" }
            },
            TotalItems = 2,
            PageNumber = 1,
            PageSize = 10
        };

        _advisorServiceMock.Setup(s => s.GetPagedAdvisorsAsync(1, 10, ""))
            .ReturnsAsync(pagedResult);

        // Act
        var result = await _controller.GetAllAdvisors("", 1, 10);

        // Assert
        var okResult = Assert.IsType<ActionResult<PagedResult<AdvisorDto>>>(result);
        Assert.IsType<OkObjectResult>(okResult.Result);

        var objectResult = okResult.Result as OkObjectResult;
        Assert.NotNull(objectResult);
        Assert.Equal(200, objectResult.StatusCode);
        Assert.Equal(pagedResult, objectResult.Value);
    }

    [Fact]
    public async Task GetAllAdvisors_ReturnsNotFound_WhenNoAdvisorsFound()
    {
        // Arrange
        _advisorServiceMock.Setup(s => s.GetPagedAdvisorsAsync(1, 10, "")).ReturnsAsync(new PagedResult<AdvisorDto>());

        // Act
        var result = await _controller.GetAllAdvisors("", 1, 10);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateAdvisor_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var updateDto = new UpdateAdvisorDto { FullName = "John Smith", SIN = "12374859" };
        _advisorServiceMock.Setup(s => s.UpdateAdvisorAsync(1, updateDto)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateAdvisor(1, updateDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateAdvisor_ReturnsNotFound_WhenAdvisorDoesNotExist()
    {
        // Arrange
        var updateDto = new UpdateAdvisorDto { FullName = "John Smith", SIN = "12374859" };
        _advisorServiceMock.Setup(s => s.UpdateAdvisorAsync(1, updateDto)).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateAdvisor(1, updateDto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteAdvisor_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        _advisorServiceMock.Setup(s => s.DeleteAdvisorAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteAdvisor(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteAdvisor_ReturnsNotFound_WhenAdvisorDoesNotExist()
    {
        // Arrange
        _advisorServiceMock.Setup(s => s.DeleteAdvisorAsync(1)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteAdvisor(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}