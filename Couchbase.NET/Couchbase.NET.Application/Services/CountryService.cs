using AutoMapper;
using Couchbase.NET.Application.Interfaces;
using Couchbase.NET.Application.ViewModels;
using Couchbase.NET.Domain.Entities;
using Couchbase.NET.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Application.Services
{
    public class CountryService: ICountryService
    {
        private readonly IMapper _mapper;

        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            this._countryRepository = countryRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CountryViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var allCountries = await this._countryRepository.GetAllAsync(cancellationToken);

            return this._mapper.Map<IEnumerable<CountryViewModel>>(allCountries);
        }

        public async Task<CountryViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var country = await this._countryRepository.GetByIdAsync(id, cancellationToken);

            return this._mapper.Map<CountryViewModel>(country);
        }

        public async Task<bool> Create(CountryViewModel model, CancellationToken cancellationToken)
        {
            return await this._countryRepository.Create(this._mapper.Map<Country>(model), cancellationToken);
        }

        public async Task<bool> Update(CountryViewModel model, CancellationToken cancellationToken)
        {
            return await this._countryRepository.Update(this._mapper.Map<Country>(model), cancellationToken);
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            return await this._countryRepository.Delete(id, cancellationToken);
        }
    }
}
