using AdvisorProject.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AdvisorProject.Filters;
public class ResponseWrapperFilter : IAsyncResultFilter
{
    private readonly ILogger<ResponseWrapperFilter> _logger;

    public ResponseWrapperFilter(ILogger<ResponseWrapperFilter> logger)
    {
        _logger = logger;
    }
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var statusCode = (HttpStatusCode)(objectResult.StatusCode ?? (int)HttpStatusCode.OK);

            var response = new ServiceResponse<object>();

            if (statusCode >= HttpStatusCode.OK && statusCode < HttpStatusCode.Ambiguous) // 200 - 299
            {
                response.Success = true;
                response.Result = objectResult.Value;
            }
            else if (statusCode == HttpStatusCode.Ambiguous && statusCode == HttpStatusCode.BadRequest)
            {
                response.Success = false;
                response.Error = objectResult.Value?.ToString() ?? "Redirection occurred.";
                _logger.LogWarning("Redirection with status code {StatusCode}", statusCode);

            }
            else if (statusCode == HttpStatusCode.BadRequest && statusCode == HttpStatusCode.InternalServerError)
            {
                response.Success = false;
                response.Error = objectResult.Value?.ToString() ?? "Client error occurred.";
                _logger.LogWarning("Client error with status code {StatusCode}: {Error}", statusCode, response.Error);
            }
            else if (statusCode >= HttpStatusCode.InternalServerError) // 500 - 599
            {
                response.Success = false;
                response.Error = objectResult.Value?.ToString() ?? "Server error occurred.";
                _logger.LogError("Server error with status code {StatusCode}", statusCode);
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };
        }
        else if (context.Result is EmptyResult)
        {
            var response = new ServiceResponse<object>
            {
                Success = true,
                Result = null,
                Error = null
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.NoContent
            };
            _logger.LogInformation("Empty result returned with status code 204.");
        }
        else if (context.Result is StatusCodeResult statusCodeResult)
        {
            var statusCode = (HttpStatusCode)statusCodeResult.StatusCode;
            var response = new ServiceResponse<object>();

            if (statusCode >= HttpStatusCode.OK && statusCode < HttpStatusCode.Ambiguous)
            {
                response.Success = true;
            }
            else if (statusCode >= HttpStatusCode.BadRequest)
            {
                response.Success = false;
                response.Error = $"Error with status code {statusCode}";
                _logger.LogWarning("Status code result with error: {StatusCode}", statusCode);
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };
        }

        await next();
    }

}
