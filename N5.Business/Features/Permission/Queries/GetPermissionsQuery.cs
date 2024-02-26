namespace N5.Business.Features.Permission.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using N5.Business.Commons.Exceptions;
    using N5.Business.Interfaces.Services;
    using N5.Core.Dtos.Permission;
    using N5.Core.Messages;
    using N5.Core.Enums;
    using N5.Core.Dtos;

    public class GetPermissionsQuery : IRequest<IEnumerable<PermissionDto>> { }

    public class GetPermissionsQueryHandler(IGroupService groupService) : IRequestHandler<GetPermissionsQuery, IEnumerable<PermissionDto>>
    {
        private readonly IGroupService _groupService = groupService;

        public async Task<IEnumerable<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var data = await _groupService.PermissionElasticsearchService.GetAllAsync() ?? throw new NotFoundException(ErrorMessage.NotFoundEntity.Replace("{entity}", "Permiso"));
            await _groupService.KafkaServices.SendServices(new KafkaDto()
            {
                Id = Guid.NewGuid(),
                NameOperation = OperationEnum.Get
            });

            return data;
        }
    }
}