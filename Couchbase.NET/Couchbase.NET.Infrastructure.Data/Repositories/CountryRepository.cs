using Couchbase.Extensions.DependencyInjection;
using Couchbase.NET.Domain.Entities;
using Couchbase.NET.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Infrastructure.Data.Repositories
{
    public class CountryRepository: BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(INamedBucketProvider bucketProvider) : base(bucketProvider) { }

        public async Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Query<Country>().ToList());
        }

        public async Task<Country> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Query<Country>().Where(c => c.Id == id).FirstOrDefault());
        }

        public async Task<bool> Create(Country model, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Bucket.Upsert<Country>(new Document<Country>
            {
                Id = model.Id.ToString(),
                Content = model
            }).Success);
        }

        public async Task<bool> Update(Country model, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Bucket.Insert<Country>(new Document<Country>
            {
                Id = model.Id.ToString(),
                Content = model
            }).Success);
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bucketContext.Bucket.Remove<Country>(new Document<Country>
            {
                Id = id.ToString(),
                Content = _bucketContext.Query<Country>().Where(c => c.Id == id).FirstOrDefault()
            }).Success);
        }
    }
}
