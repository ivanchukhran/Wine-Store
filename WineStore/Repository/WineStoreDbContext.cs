using System;
using Microsoft.EntityFrameworkCore;
using WineStore.Repository.Models;

namespace WineStore.Repository;

public sealed class WineStoreDbContext : DbContext
{
    public DbSet<Wine> Wines { get; set; } = null!;
    public DbSet<User?> Users { get; set; } = null!;
    
    public WineStoreDbContext()
    {
        Database.EnsureCreated();
        Console.WriteLine("Database created");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=mysecretpassword;Database=postgres");
    }
}