using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WineStore.Repository.Models;
using WineStore.ViewModels;

namespace WineStore.Repository.Providers;

public class WineService
{
    public static async void BuyWineAsync(Wine wine, int userId)
    {
        using (var dbContext = new WineStoreDbContext())
        {
            var wineFound = await dbContext.Wines.FindAsync(wine.Id);
            if (wineFound == null)
            {
                return;
            }

            wineFound.UserId = userId;
            await dbContext.SaveChangesAsync();
        }
    }

    public static async void AddWineAsync(Wine wine)
    {
        using (var dbContext = new WineStoreDbContext())
        {
            await dbContext.Wines.AddAsync(wine);
            await dbContext.SaveChangesAsync();
        }
    }
    
    public static async void AddRangeAsync(List<Wine> wines)
    {
        await using var dbContext = new WineStoreDbContext();
        await dbContext.Wines.AddRangeAsync(wines);
        await dbContext.SaveChangesAsync();
    }

    public static async void RemoveWineAsync(Wine wine)
    {
        using (var dbContext = new WineStoreDbContext())
        {
            var wineFound = await dbContext.Wines.FindAsync(wine.Id);
            if (wineFound == null)
            {
                return;
            }

            dbContext.Wines.Remove(wineFound);
            await dbContext.SaveChangesAsync();
        }
    }

    public static async Task<List<Wine>> GetStoreWinesAsync()
    {
        using (var dbContext = new WineStoreDbContext())
        {
            return await dbContext.Wines.Where(w => w.UserId == null).ToListAsync();
        }
    }

    public static async Task<List<Wine>> GetUserWinesAsync(int userId)
    {
        using (var dbContext = new WineStoreDbContext())
        {
            return await dbContext.Wines.Where(w => w.UserId == userId).ToListAsync();
        }
    }
}