namespace N5.Business.Features.PermissionType.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using N5.Business.Commons.Exceptions;
    using N5.Business.Interfaces.DataAccess;
    using N5.Core.Dtos.PermissionType;
    using N5.Core.Messages;

    public class PermissionTypeDeleteCommand : PermissionTypeByIdDto, IRequest { }

    public class PermissionTypeDeleteCommandHandlerHandler(IUnitOfWork unitOfWork) : IRequestHandler<PermissionTypeDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(PermissionTypeDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _unitOfWork.PermissionTypeRepository.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new BadRequestException(ErrorMessage.NotFoundEntityById
                   .Replace("{entity}", "Tipo de permiso")
                   .Replace("{id}", request.Id.ToString()));
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.PermissionTypeRepository.DeleteAsync(data);
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

