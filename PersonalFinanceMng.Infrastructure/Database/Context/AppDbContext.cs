using Microsoft.EntityFrameworkCore;
using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Infrastructure.Database.Configurations;

namespace PersonalFinanceMng.Infrastructure.Database.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Registra a classe de configuração explicitamente
        modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
    }
}