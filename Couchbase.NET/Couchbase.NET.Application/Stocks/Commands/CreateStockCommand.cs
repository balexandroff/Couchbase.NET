using Couchbase.NET.Application.Common;
using Couchbase.NET.Application.Interfaces;
using Couchbase.NET.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Application.Stocks.Commands
{
    public class CreateStockCommand : IRequestWrapper<bool>
    {
        public StockViewModel Model { get; private set; }

        public CreateStockCommand(StockViewModel model)
        {
            Model = model;
        }
    }

    public class CreateStockCommandHandler : IRequestHandlerWrapper<CreateStockCommand, bool>
    {
        private readonly IStockService _stockService;

        public CreateStockCommandHandler(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<ServiceResult<bool>> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var result = await _stockService.Create(request.Model, cancellationToken);

            return result ? ServiceResult.Success(result) : ServiceResult.Failed<bool>(ServiceError.NotFound);
        }
    }
}
