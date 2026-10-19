using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Domain;
using PersonalFinanceMng.Domain.Repositories;
using PersonalFinanceMng.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceMng.Infrastructure.Database.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly AppDbContext _context;

    public PaymentMethodRepository(AppDbContext context)
    {
        _context = context;
    }

    // CREATE
    public async Task AddAsync(
        PaymentMethod paymentMethod, 
        CancellationToken cancellationToken = default    
    )
    {
        await _context.Set<PaymentMethod>()
            .AddAsync(paymentMethod, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }


    // UPDATE
    public async Task<PaymentMethod> UpdateAsync(
        Guid userId,
        PaymentMethod paymentMethod, 
        CancellationToken cancellationToken = default
    )
    {
        var entry = _context.Set<PaymentMethod>().Update(paymentMethod);
        await _context.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }


    // READ (Get All By User)
    public async Task<IEnumerable<PaymentMethod>> GetAllByUserIdAsync(
        Guid userId, 
        CancellationToken cancellationToken = default
    )
    {
        return await _context.Set<PaymentMethod>()
            .Where(p => p.UserId == userId || p.UserId == null)
            .ToListAsync(cancellationToken);
    }


    //Delete permanently
    public async Task HardDeleteAsync(
        Guid userId, 
        PaymentMethod paymentMethod, 
        CancellationToken cancellationToken = default
    )
    {
        // Remove o registro do contexto de rastreamento
        _context.Set<PaymentMethod>().Remove(paymentMethod);

        // Persiste a remoção física no banco de dados de forma assíncrona
        await _context.SaveChangesAsync(cancellationToken);
    }


    // turns IsActive false;
    public async Task SoftDeleteAsync(
        Guid userId, 
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken = default
    )
    {
        paymentMethod.Deactivate();
        _context.Set<PaymentMethod>().Update(paymentMethod);
        await _context.SaveChangesAsync(cancellationToken);
    }


    async public Task<PaymentMethod?> GetByIdAsync(
        Guid userId, 
        int paymentMethodId, 
        CancellationToken cancellationToken = default
    )
    {
          return await _context.Set<PaymentMethod>()
         .FirstOrDefaultAsync(
              p => p.Id == paymentMethodId 
              && (p.UserId == userId || p.UserId == null),
              cancellationToken
         );
    }


}