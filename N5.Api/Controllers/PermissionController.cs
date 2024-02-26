namespace N5.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using N5.Business.Features.Permission.Commands;
    using N5.Business.Features.Permission.Queries;
    using N5.Core.Dtos.Permission;

    [ApiController]
    [Route("api/V1/[controller]")]
    public class PermissionController : ControllerBase
    {
        #region Properties
        private readonly IMediator _mediator;
        #endregion

        public PermissionController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get Permissions.
        /// </summary>
        /// <returns>List of permission.</returns>
        [HttpGet("GetPermissions")]
        public Task<IEnumerable<PermissionDto>> GetPermissions() => _mediator.Send(new GetPermissionsQuery());

        /// <summary>
        /// Create Permission.
        /// </summary>
        /// <param name="requestCommand">The param.</param>
        /// <returns>Unit</returns>
        [HttpPost("RequestPermission")]
        public async Task<Unit> RequestPermissionAsync([FromBody] RequestCommand requestCommand) => await _mediator.Send(requestCommand);

        /// <summary>
        /// Update Permission.
        /// </summary>
        /// <param name="modifyPermissionCommand">The param.</param>
        /// <returns>Unit</returns>
        [HttpPut("ModifyPermission")]
        public async Task<Unit> ModifyPermissionAsync([FromBody] ModifyPermissionCommand modifyPermissionCommand) => await _mediator.Send(modifyPermissionCommand);

    }
}