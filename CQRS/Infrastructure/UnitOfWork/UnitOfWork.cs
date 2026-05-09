using CQRS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CQRS.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private IDbContextTransaction _transaction;
        public UnitOfWork( ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();

            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
           _transaction?.Dispose();
            _context?.Dispose();
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
               await _transaction.DisposeAsync();
                _transaction = null;

            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
