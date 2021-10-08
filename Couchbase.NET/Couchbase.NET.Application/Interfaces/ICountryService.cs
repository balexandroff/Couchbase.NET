using Couchbase.NET.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Application.Interfaces
{
    public interface ICountryService: IService
    {
        public Task<IEnumerable<CountryViewModel>> GetAllAsync(CancellationToken cancellationToken);
        public Task<CountryViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<bool> Create(CountryViewModel model, CancellationToken cancellationToken);
        public Task<bool> Update(CountryViewModel model, CancellationToken cancellationToken);
        public Task<bool> Delete(Guid id, CancellationToken cancellationToken);
    }
}
