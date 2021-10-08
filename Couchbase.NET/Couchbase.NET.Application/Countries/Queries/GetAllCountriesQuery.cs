using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Couchbase.NET.Application.Common;
using Couchbase.NET.Application.Interfaces;
using Couchbase.NET.Application.ViewModels;

namespace Couchbase.NET.Application.Countries.Queries
{
    public class GetAllCountriesQuery : IRequestWrapper<IEnumerable<CountryViewModel>>
    {

    }

    public class GetAllCountriesQueryHandler : IRequestHandlerWrapper<GetAllCountriesQuery, IEnumerable<CountryViewModel>>
    {
        private readonly ICountryService _countryService;

        public GetAllCountriesQueryHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<ServiceResult<IEnumerable<CountryViewModel>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var list = await this._countryService.GetAllAsync(cancellationToken);

            return list.Count() > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<IEnumerable<CountryViewModel>>(ServiceError.NotFound);
        }
    }
}
