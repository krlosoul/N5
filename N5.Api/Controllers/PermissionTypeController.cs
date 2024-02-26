namespace N5.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using N5.Business.Features.PermissionType.Commands;
    using N5.Business.Features.PermissionType.Queries;
    using N5.Core.Dtos.PermissionType;

    [ApiController]
    [Route("api/V1/[controller]")]
    public class PermissionTypeController : ControllerBase
    {
        #region Properties
        private readonly IMediator _mediator;
        #endregion

        public PermissionTypeController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get Permission type.
        /// </summary>
        /// <returns>List of permission type.</returns>
        [HttpGet("GetPermissionTypes")]
        public Task<IEnumerable<PermissionTypeDto>> GetAllAsync() => _mediator.Send(new PermissionTypeGetAllQuery());

        /// <summary>
        /// Get Permission type by id.
        /// </summary>
        /// <param name="getByIdQuery">The param.</param>
        /// <returns>The PermissionType.</returns>
        [HttpGet("GetById/{Id}")]
        public Task<PermissionTypeDto> GetByIdAsync([FromRoute] PermissionTypeGetByIdQuery getByIdQuery) => _mediator.Send(getByIdQuery);

        /// <summary>
        /// Create Permission type.
        /// </summary>
        /// <param name="createCommand">The param.</param>
        /// <returns>Unit</returns>
        [HttpPost("Create")]
        public async Task<Unit> CreateAsync([FromBody] PermissionTypeCreateCommand createCommand) => await _mediator.Send(createCommand);

        /// <summary>
        /// Update Permission type.
        /// </summary>
        /// <param name="updateCommand">The param.</param>
        /// <returns>Unit</returns>
        [HttpPut("Update")]
        public async Task<Unit> UpdateAsync([FromBody] PermissionTypeUpdateCommand updateCommand) => await _mediator.Send(updateCommand);

        /// <summary>
        /// Delete Permission type.
        /// </summary>
        /// <param name="deleteCommand">The param.</param>
        /// <returns>Unit</returns>
        [HttpDelete("Delete/{Id}")]
        public async Task<Unit> DeleteAsync([FromRoute] PermissionTypeDeleteCommand deleteCommand) => await _mediator.Send(deleteCommand);
    }
}