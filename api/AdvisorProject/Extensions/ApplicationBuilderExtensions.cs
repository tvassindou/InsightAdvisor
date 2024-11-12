
using AdvisorProject.Infrastructure.Data;
using AdvisorProject.Infrastructure.Seed;

namespace AdvisorProject.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void InitialDbBuilder(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AdvisorDbContext>();
    
        if (!context.Advisors.Any())
        {
            var dbSeeder = new InitialDbBuilder(context);
            dbSeeder.Create();
        }
    }
}