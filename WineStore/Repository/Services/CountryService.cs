using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WineStore.Repository.Models;

namespace WineStore.Repository.Providers;

public class CountryService
{
    public static async void CreateAsync(Country country)
    {
        await using var dbContext = new WineStoreDbContext();
        await dbContext.Countries.AddAsync(country);
        await dbContext.SaveChangesAsync();
    }
    
    public static async Task<Country?> GetCountryAsync(int id)
    {
        await using var dbContext = new WineStoreDbContext();
        return await dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public static async void AddRangeAsync(List<Country> countries)
    {
        await using var dbContext = new WineStoreDbContext();
        await dbContext.Countries.AddRangeAsync(countries);
        await dbContext.SaveChangesAsync();
    }
    
}