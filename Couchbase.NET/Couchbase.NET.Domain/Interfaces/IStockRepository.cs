using Couchbase.NET.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Domain.Interfaces
{
    public interface IStockRepository : IRepository
    {
        public Task<IEnumerable<Stock>> GetAllAsync(CancellationToken cancellationToken);
        public Task<Stock> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<bool> Create(Stock model, CancellationToken cancellationToken);
        public Task<bool> Update(Stock model, CancellationToken cancellationToken);
        public Task<bool> Delete(Guid id, CancellationToken cancellationToken);
    }
}
