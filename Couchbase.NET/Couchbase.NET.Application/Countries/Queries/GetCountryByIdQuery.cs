using Couchbase.NET.Application.Common;
using Couchbase.NET.Application.Interfaces;
using Couchbase.NET.Application.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Application.Countries.Queries
{
    public class GetCountryByIdQuery : IRequestWrapper<CountryViewModel>
    {
        public Guid Id { get; private set; }

        public GetCountryByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetCountryByIdQueryHandler : IRequestHandlerWrapper<GetCountryByIdQuery, CountryViewModel>
    {
        private readonly ICountryService _countryService;

        public GetCountryByIdQueryHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<ServiceResult<CountryViewModel>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var country = await this._countryService.GetByIdAsync(request.Id, cancellationToken);

            return country != null ? ServiceResult.Success(country) : ServiceResult.Failed<CountryViewModel>(ServiceError.NotFound);
        }
    }
}
