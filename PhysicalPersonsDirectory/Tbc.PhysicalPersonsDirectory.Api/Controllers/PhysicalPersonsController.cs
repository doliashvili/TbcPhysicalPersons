using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tbc.PhysicalPersonsDirectory.Api.Infrastructure.Middlewares.ErrorHandling;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Create;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Create.Model;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.CreateRelation;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.CreateRelation.Model;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Delete;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Delete.Model;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.DeleteRelation;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.DeleteRelation.Model;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Update;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Update.Model;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.UploadImage;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.UploadImage.Model;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetByIdIncludedData;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetByIdIncludedData.Model;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData.Model;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetFilteredPagedData;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetFilteredPagedData.Model;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetReport;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetReport.Model;

namespace Tbc.PhysicalPersonsDirectory.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhysicalPersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhysicalPersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatePhysicalPersonResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<CreatePhysicalPersonResponse>> CreatePhysicalPerson([FromBody] CreatePhysicalPersonCommand request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletePhysicalPersonResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<DeletePhysicalPersonResponse>> DeletePhysicalPerson([FromBody] DeletePhysicalPersonCommand request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatePhysicalPersonResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<UpdatePhysicalPersonResponse>> UpdatePhysicalPerson([FromBody] UpdatePhysicalPersonCommand request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UploadImageResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<UploadImageResponse>> UploadPhysicalPersonImage([FromForm] UploadImageCommand request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateRelationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<CreateRelationResponse>> CreateRelation([FromBody] CreateRelationCommand request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteRelationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<DeleteRelationResponse>> DeleteRelation([FromBody] DeleteRelationCommand request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdIncludedDataResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<GetByIdIncludedDataResponse>> GetById([FromQuery] GetByIdIncludedDataQuery request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDetailedFilteredPagedDataQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<GetDetailedFilteredPagedDataQueryResponse>> GetDetailedFilteredPagedData([FromQuery] GetDetailedFilteredPagedDataQuery request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFilteredPagedDataQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<GetFilteredPagedDataQueryResponse>> GetFilteredPagedData([FromQuery] GetFilteredPagedDataQuery request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReportQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
        public async Task<ActionResult<GetReportQueryResponse>> GetReport([FromQuery] GetReportQuery request)
            => Ok(await _mediator.Send(request).ConfigureAwait(false));
    }
}