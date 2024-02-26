namespace N5.Business.Features.Permission.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using N5.Business.Commons.Exceptions;
    using N5.Business.Interfaces.DataAccess;
    using N5.Business.Interfaces.Services;
    using N5.Core.Dtos;
    using N5.Core.Dtos.Permission;
    using N5.Core.Entities;
    using N5.Core.Enums;
    using N5.Core.Messages;

    public class ModifyPermissionCommand : PermissionDto, IRequest { }

    public class ModifyPermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IGroupService groupService) : IRequestHandler<ModifyPermissionCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IGroupService? _groupService = groupService;

        public async Task<Unit> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await groupService.PermissionElasticsearchService.FirstAsync(request.Id);
                if (data == null) throw new BadRequestException(ErrorMessage.NotFoundEntityById.Replace("{entity}", "Permiso").Replace("{id}", request.Id.ToString()));
                var permission = _mapper.Map<Permission>(data);
                bool update = await _unitOfWork.PermissionRepository.UpdateAsync(permission);
                await _unitOfWork.CommitTransactionAsync();
                if (update)
                {
                    await _groupService.PermissionElasticsearchService.UpdateAsync(_mapper.Map<PermissionDto>(permission), request.Id);
                    await _groupService.KafkaServices.SendServices(new KafkaDto()
                    {
                        Id = Guid.NewGuid(),
                        NameOperation = OperationEnum.Request
                    });
                }


                return Unit.Value;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _unitOfWork.CloseTransactionAsync();
            }
        }
    }
}

