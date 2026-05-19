using System.Text.Json;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;

namespace LibraryMS.Infrastructure.SeedData;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(this AppDbContext context)
    {
        if(!context.Countries.Any())
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SeedData", "countries.json");

            if (File.Exists(path))
            {
                var countriesData = await File.ReadAllTextAsync(path);
                
                var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
                var countries = JsonSerializer.Deserialize<List<Country>>(countriesData, options);

                if (countries != null)
                {
                    await context.Countries.AddRangeAsync(countries);
                    await context.SaveChangesAsync();
                }
            }
        }
        
        if(!context.Settings.Any())
        {
            context.Settings.Add(new Setting
            {
                DefaultBorrowDays = 7,
                DefaultFinePerDay = 1.1m
            });
            
            await context.SaveChangesAsync();
        }
    }
}