namespace N5.Business.Features.Permission.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using N5.Business.Interfaces.DataAccess;
    using N5.Business.Interfaces.Services;
    using N5.Core.Dtos;
    using N5.Core.Dtos.Permission;
    using N5.Core.Enums;
    using N5.Core.Entities;

    public class RequestCommand : PermissionCreateDto, IRequest { }

    public class RequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IGroupService groupService) : IRequestHandler<RequestCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IGroupService _groupService = groupService;

        public async Task<Unit> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var permission = _mapper.Map<PermissionCreateDto, Permission>(request);
                await _unitOfWork.BeginTransactionAsync();
                bool insert = await _unitOfWork.PermissionRepository.InsertAsync(permission);
                await _unitOfWork.CommitTransactionAsync();
                if (insert)
                {
                    await _groupService.PermissionElasticsearchService.InsertAsync(_mapper.Map<PermissionDto>(permission));
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

