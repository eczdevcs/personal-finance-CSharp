using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Infrastructure.Database.Context;
using PersonalFinanceMng.Infrastructure.Database.Repositories; // Ajuste conforme a sua classe de repositório
using FluentAssertions;
using Xunit;
using FluentAssertions.Equivalency;
using System.Runtime.InteropServices;

namespace PersonalFinanceMng.Tests.Integration.Repository
{
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
    public class RepositoryCollection { }

    [Collection("Non-Parallel Collection")]
    public class PaymentMethodRepositoryTests
    {
        private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=PersonalFinanceDbTest;Trusted_Connection=True;MultipleActiveResultSets=true";

        private DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_connectionString)
            .Options;
        }

        private async Task ResetDatabaseAsync(AppDbContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        [Fact]
        public async Task AddAsync_ShoudPersistPaymentMethodSuccessfuly()
        {
            //Arrange
            var options = CreateNewContextOptions();
            using var context = new AppDbContext(options);
            await ResetDatabaseAsync(context);

            var repository = new PaymentMethodRepository(context);

            var paymentMethod = new PaymentMethod("Cartão de Crédito", "Uso pessoal", 1);

            //Act
            await repository.AddAsync(paymentMethod);

            await context.SaveChangesAsync();

            using var assertContext = new AppDbContext(options);

            var result = await assertContext.PaymentMethods.FirstOrDefaultAsync(p => p.Id == paymentMethod.Id);
            //Assertions
            result.Should().NotBeNull();
            result!.Name.Should().Be("Cartão de Crédito");
            result.UserId.Should().Be(1);
            result.IsActive.Should().BeTrue();
        }

        [Fact]
        public async Task AddAsync_ShouldThrowDbUpdateException_WhenDuplicatedUserIdAndName()
        {
            //Arrange
            var options = CreateNewContextOptions();
            using var context = new AppDbContext(options);
            await ResetDatabaseAsync(context);

            var repository = new PaymentMethodRepository(context);

            var item1 = new PaymentMethod("Pix", "Pagamento eletronico", 1, true);
            var item2 = new PaymentMethod("Pix", "Pagamento eletronico", 1, true);

            // Act
            await repository.AddAsync(item1);
           
            // Assert
            var action = async () => await repository.AddAsync(item2);

            await action.Should().ThrowAsync<DbUpdateException>();

        }

        [Fact]

        public async Task GetByIdAsync_ShouldReturnPaymentMethod_WhenExists()
        {
            //Arrange
            var options = CreateNewContextOptions();
            using var context = new AppDbContext(options);
            await ResetDatabaseAsync(context);
            var repository = new PaymentMethodRepository(context);

            var entity = new PaymentMethod("Pix", "Pagamento instantaneo", 1);

            await repository.AddAsync(entity);
            await context.SaveChangesAsync();

            //Act
            var result = await repository.GetByIdAsync(entity.Id);

            //Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Pix");
        }

        [Fact]
        public async Task UpdateAsync_ShoudUpdateEntitySuccessfully()
        {
            //Arrange
            var options = CreateNewContextOptions();
            using var context = new AppDbContext(options);
            await ResetDatabaseAsync(context);
            var repository = new PaymentMethodRepository(context);

            var entity = new PaymentMethod("Pix", "Pagamento instantaneo", 1);

            await repository.AddAsync(entity);
            await context.SaveChangesAsync();
            //Act
            entity.UpdateDetails("VR", "Vale Refeicao");
            await repository.UpdateAsync(entity);

            // Assert
            using var assertContext = new AppDbContext(options);
            var updated = await assertContext.PaymentMethods.FindAsync(entity.Id);
            updated.Should().NotBeNull();
            updated!.Name.Should().Be("VR");
            updated.Description.Should().Be("Vale Refeicao");
        }

        [Fact]
        public async Task softDeleteAsync_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new AppDbContext(options);
            await ResetDatabaseAsync(context);
            var repository = new PaymentMethodRepository(context);
            var entity = new PaymentMethod("Pix", "Pagamento instantaneo", 1);
            await repository.AddAsync(entity);

            // Act
                await repository.SoftDeleteAsync(entity);

            // Assertion
            using var assertContext = new AppDbContext(options);
            var repo = new PaymentMethodRepository(assertContext);
            var newEntity = await repo.GetByIdAsync(entity.Id);
            newEntity.Should().BeNull();
        }

        [Fact]
        public async Task HardDeleteAsync_ShouldRemoveRecord()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new AppDbContext(options);
            await ResetDatabaseAsync(context);
            var repository = new PaymentMethodRepository(context);
            var entity = new PaymentMethod("Pix", "Pagamento instantaneo", 1);
            await repository.AddAsync(entity);

            // Act
            await repository.HardDeleteAsync(entity);

            // Assertion
            using var assertContext = new AppDbContext(options);

            // Consulta direta na tabela desativando filtros de consulta
            var dbEntity = await assertContext.PaymentMethods
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == entity.Id);

            dbEntity.Should().BeNull();
        }

    }
}
