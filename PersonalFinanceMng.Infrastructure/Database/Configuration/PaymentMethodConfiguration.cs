using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceMng.Domain.Entities;

namespace PersonalFinanceMng.Infrastructure.Database.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethods");

        builder.HasKey(p => p.Id);
        // Filtra automaticamente entidades inativas em todas as consultas
        builder.HasQueryFilter(p => p.IsActive);

        builder.Property(p => p.Id)
       .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(100);

        // Ignora a propriedade NormalizedName para NÃO criar a coluna no banco de dados
        builder.Ignore(p => p.NormalizedName);

        // Cria o índice único por Usuário e Nome do Método de Pagamento
        builder.HasIndex(p => new { p.UserId, p.Name })
            .IsUnique();
    }
}