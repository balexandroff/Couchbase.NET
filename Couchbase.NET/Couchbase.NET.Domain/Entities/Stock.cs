using System;

namespace Couchbase.NET.Domain.Entities
{
    public class Stock: AudutableEntity
    {
        public string Ticker { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
