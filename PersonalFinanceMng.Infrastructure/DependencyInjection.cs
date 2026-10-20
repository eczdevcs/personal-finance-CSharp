using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceMng.Domain.Repositories;
using PersonalFinanceMng.Infrastructure.Database.Context;
using PersonalFinanceMng.Infrastructure.Database.Repositories;


namespace PersonalFinanceMng.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // aqui sera registrado o entity framework/dapper repositorios e servicos externos 
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada no appsettings.json!");
            }

            services.AddScoped<IUserRepository, UserRepsitoryMOCK>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>(); // Adicione esta linha
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

            return services;
        }
    }
}
