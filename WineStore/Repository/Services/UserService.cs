using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WineStore.Repository.Models;

namespace WineStore.Repository.Providers;

public class UserService
{
    public static async Task<User> CreateAsync(string username, string password)
    {
        var user = new User
        {
            Username = username,
            Password = password
        };
        using (var dbContext = new WineStoreDbContext())
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }
        return user;
    }
    
    public static async Task<User?> GetUserAsync(int id)
    {
        using (var dbContext = new WineStoreDbContext())
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
    
    public static async Task<User> GetUserAsync(string username, string password)
    {
        using (var dbContext = new WineStoreDbContext())
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }

}