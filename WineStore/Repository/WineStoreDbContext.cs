using System;
using Microsoft.EntityFrameworkCore;
using WineStore.Repository.Models;

namespace WineStore.Repository;

public sealed class WineStoreDbContext : DbContext
{
    public DbSet<Wine?> Wines { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    
    public WineStoreDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=mysecretpassword;Database=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wine>().HasKey(w => w.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Country>().HasKey(c => c.Id);

        modelBuilder.Entity<Wine>()
            .HasOne(w => w.User)
            .WithMany(u => u.Wines);
        
        modelBuilder.Entity<Country>()
            .HasMany(c => c.Wines)
            .WithOne(w => w.Country);

    }
}