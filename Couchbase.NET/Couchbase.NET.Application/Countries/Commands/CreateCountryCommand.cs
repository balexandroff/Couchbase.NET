using Couchbase.NET.Application.Common;
using Couchbase.NET.Application.Interfaces;
using Couchbase.NET.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Application.Countries.Commands
{
    public class CreateCountryCommand : IRequestWrapper<bool>
    {
        public CountryViewModel Model { get; private set; }

        public CreateCountryCommand(CountryViewModel model)
        {
            Model = model;
        }
    }

    public class CreateCountryCommandHandler : IRequestHandlerWrapper<CreateCountryCommand, bool>
    {
        private readonly ICountryService _countryService;

        public CreateCountryCommandHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<ServiceResult<bool>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var result = await _countryService.Create(request.Model, cancellationToken);

            return result ? ServiceResult.Success(result) : ServiceResult.Failed<bool>(ServiceError.NotFound);
        }
    }
}
