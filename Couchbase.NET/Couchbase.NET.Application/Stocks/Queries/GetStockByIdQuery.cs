using Couchbase.NET.Application.Common;
using Couchbase.NET.Application.Interfaces;
using Couchbase.NET.Application.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.Application.Stocks.Queries
{
    public class GetStockByIdQuery : IRequestWrapper<StockViewModel>
    {
        public Guid Id { get; private set; }

        public GetStockByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetStockByIdQueryHandler : IRequestHandlerWrapper<GetStockByIdQuery, StockViewModel>
    {
        private readonly IStockService _stockService;

        public GetStockByIdQueryHandler(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<ServiceResult<StockViewModel>> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
        {
            var country = await this._stockService.GetByIdAsync(request.Id, cancellationToken);

            return country != null ? ServiceResult.Success(country) : ServiceResult.Failed<StockViewModel>(ServiceError.NotFound);
        }
    }
}
