﻿using e_Estoque_API.Core.Entities;
using e_Estoque_API.Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace e_Estoque_API.Infrastructure.Persistence;

public class EstoqueDbContext : DbContext
{
    public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyAddress> CompanyAddress { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Inventory> inventories { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleProduct> SaleProducts { get; set; }
    public DbSet<Tax> Taxs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        //optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<ZonedDateTime>(x => x.HaveConversion<ZonedDateTimeConverter>());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryMapping).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                entry.Property("CreatedAt").IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

internal class ZonedDateTimeConverter : ValueConverter<ZonedDateTime, LocalDateTime>
{
    public ZonedDateTimeConverter() :
       base(v => v.WithZone(DateTimeZone.Utc).LocalDateTime, v => v.InUtc())
    {
    }
}