namespace N5.Business.Features.PermissionType.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using N5.Business.Interfaces.DataAccess;
    using N5.Core.Dtos.PermissionType;
    using N5.Core.Entities;

    public class PermissionTypeCreateCommand : PermissionTypeCreateDto, IRequest { }

    public class PermissionTypeCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<PermissionTypeCreateCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Unit> Handle(PermissionTypeCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var permissionType = _mapper.Map<PermissionTypeCreateDto,PermissionType>(request);
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.PermissionTypeRepository.InsertAsync(permissionType);
                await _unitOfWork.CommitTransactionAsync();
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

