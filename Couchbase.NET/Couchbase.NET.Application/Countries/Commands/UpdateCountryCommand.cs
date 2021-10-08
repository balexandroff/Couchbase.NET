using Couchbase.NET.Application.Common;
using Couchbase.NET.Application.Interfaces;
using Couchbase.NET.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Application.Countries.Commands
{
    public class UpdateCountryCommand : IRequestWrapper<bool>
    {
        public CountryViewModel Model { get; private set; }

        public UpdateCountryCommand(CountryViewModel model)
        {
            Model = model;
        }
    }

    public class UpdateCountryCommandHandler : IRequestHandlerWrapper<UpdateCountryCommand, bool>
    {
        private readonly ICountryService _countryService;

        public UpdateCountryCommandHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<ServiceResult<bool>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var result = await _countryService.Update(request.Model, cancellationToken);

            return result ? ServiceResult.Success(result) : ServiceResult.Failed<bool>(ServiceError.NotFound);
        }
    }
}
