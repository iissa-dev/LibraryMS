using System.Text.Json;

namespace LibraryMS.Infrastructure.SeedData;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(this AppDbContext context, RoleManager<ApplicationRole> roleManager)
    {
        if (!context.Countries.Any())
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SeedData", "countries.json");

            if (File.Exists(path))
            {
                var countriesData = await File.ReadAllTextAsync(path);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var countries = JsonSerializer.Deserialize<List<Country>>(countriesData, options);

                if (countries != null)
                {
                    await context.Countries.AddRangeAsync(countries);
                    await context.SaveChangesAsync();
                }
            }
        }

        if (!context.Settings.Any())
        {
            context.Settings.Add(new Setting
            {
                DefaultBorrowDays = 7,
                DefaultFinePerDay = 1.1m
            });

            await context.SaveChangesAsync();
        }

        if (!roleManager.Roles.Any())
        {
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole { Name = nameof(Roles.Admin) },
                new ApplicationRole { Name = nameof(Roles.Client) },
                new ApplicationRole { Name = nameof(Roles.Employee) }
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}