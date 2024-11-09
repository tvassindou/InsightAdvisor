using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AdvisorProject.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
namespace AdvisorProject.Tests;
public class ErrorHandlingMiddlewareTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ErrorHandlingMiddlewareTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Middleware_Returns_ErrorResponse_On_Exception()
    {
        // Déclencher une route de l'API qui jette une exception pour tester le middleware
        var response = await _client.GetAsync("/api/advisors/throwerror"); // Assurez-vous que cette route jette une exception dans le contrôleur

        // Vérifier le code de statut et le format de la réponse
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<object>>();
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("An unexpected error occurred. Please try again later.", result.Error);
        Assert.Null(result.Result);
    }
}