using Couchbase.Extensions.DependencyInjection;
using Couchbase.Linq;
using Couchbase.NET.Domain.Entities;
using Couchbase.NET.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Infrastructure.Data.Repositories
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(INamedBucketProvider bucketProvider) : base(bucketProvider) { }

        public async Task<IEnumerable<Stock>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Query<Stock>().ToList());
        }

        public async Task<Stock> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Query<Stock>().Where(c => c.Id == id).FirstOrDefault());
        }

        public async Task<bool> Create(Stock model, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Bucket.Upsert<Stock>(new Document<Stock>
            {
                Id = model.Id.ToString(),
                Content = model
            }).Success);
        }

        public async Task<bool> Update(Stock model, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Bucket.Insert<Stock>(new Document<Stock>
            {
                Id = model.Id.ToString(),
                Content = model
            }).Success);
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Bucket.Remove<Stock>(new Document<Stock>
            {
                Id = id.ToString(),
                Content = _bucketContext.Query<Stock>().Where(c => c.Id == id).FirstOrDefault()
            }).Success);
        }
    }
}
