using System.Collections.Generic;

namespace Couchbase.NET.Domain.Entities
{
    public class Country : AudutableEntity
    {
        public string Name { get; set; }

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
