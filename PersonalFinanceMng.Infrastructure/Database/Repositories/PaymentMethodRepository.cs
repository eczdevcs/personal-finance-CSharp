using PersonalFinanceMng.Domain.Entities;
using PersonalFinanceMng.Domain;
using PersonalFinanceMng.Domain.Repositories;
using PersonalFinanceMng.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceMng.Infrastructure.Database.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly  AppDbContext _context;

    public PaymentMethodRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default)
    {
        await _context.Set<PaymentMethod>()
              .AddAsync(paymentMethod, cancellationToken);
    }

    public void Delete(PaymentMethod paymentMethod)
    {
        _context.Set<PaymentMethod>()
            .Remove(paymentMethod);
    }

    public  async Task<IEnumerable<PaymentMethod>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<PaymentMethod>()
            .Where(p => p.UserId == userId || p.UserId == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<PaymentMethod?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<PaymentMethod>()
           .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void Update(PaymentMethod paymentMethod)
    {
        _context.Set<PaymentMethod>().
            Update(paymentMethod);
    }
}