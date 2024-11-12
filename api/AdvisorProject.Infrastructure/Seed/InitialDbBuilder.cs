using System.Reflection;
using System.Text.Json;
using AdvisorProject.Core.Entities;
using AdvisorProject.Infrastructure.Data;

namespace AdvisorProject.Infrastructure.Seed;

public class InitialDbBuilder
{
    private AdvisorDbContext _context;

    public InitialDbBuilder(AdvisorDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        var advisors = LoadAdvisorsFromEmbeddedResource();
        if (advisors != null)
        {
            _context.Advisors.AddRange(advisors);
            _context.SaveChanges();
        }
    }

    private List<Advisor>? LoadAdvisorsFromEmbeddedResource()
    {
        string resourcePath = "AdvisorProject.Infrastructure.Seed.SeedData.json";
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourcePath);
        if (stream == null)
            throw new FileNotFoundException($"Resource '{resourcePath}' not found.");

        using var reader = new StreamReader(stream);
        var jsonData = reader.ReadToEnd();

        return JsonSerializer.Deserialize<List<Advisor>>(jsonData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}