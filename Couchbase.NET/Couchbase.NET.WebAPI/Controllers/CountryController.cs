using Couchbase.NET.Application.Common;
using Couchbase.NET.Application.Countries.Commands;
using Couchbase.NET.Application.Countries.Queries;
using Couchbase.NET.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Couchbase.NET.API.Controllers
{
    //[Authorize]
    public class CountryController : BaseController
    {

        [HttpGet("getall")]
        public async Task<ActionResult<ServiceResult<IEnumerable<CountryViewModel>>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllCountriesQuery(), cancellationToken));
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<ServiceResult<IEnumerable<CountryViewModel>>>> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetCountryByIdQuery(id), cancellationToken));
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResult<IEnumerable<CountryViewModel>>>> Create([FromBody] CountryViewModel model, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new CreateCountryCommand(model), cancellationToken));
        }

        [HttpPost("update")]
        public async Task<ActionResult<ServiceResult<IEnumerable<CountryViewModel>>>> Update([FromBody] CountryViewModel model, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new UpdateCountryCommand(model), cancellationToken));
        }

        [HttpPost("delete/{id}")]
        public async Task<ActionResult<ServiceResult<IEnumerable<CountryViewModel>>>> Delete(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new DeleteCountryCommand(id), cancellationToken));
        }
    }
}
