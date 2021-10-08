using Couchbase.Extensions.DependencyInjection;
using Couchbase.Linq;
using Couchbase.NET.Domain.Entities;

namespace Couchbase.NET.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> where T : AudutableEntity
    {
        protected readonly IBucketContext _bucketContext;

        public BaseRepository(INamedBucketProvider bucketProvider)
        {
            _bucketContext = new BucketContext(bucketProvider.GetBucket());
        }
    }
}
