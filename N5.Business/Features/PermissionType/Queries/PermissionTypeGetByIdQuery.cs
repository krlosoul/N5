namespace N5.Business.Features.PermissionType.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using N5.Business.Commons.Exceptions;
    using N5.Business.Interfaces.DataAccess;
    using N5.Core.Dtos.PermissionType;
    using N5.Core.Entities;
    using N5.Core.Messages;

    public class PermissionTypeGetByIdQuery: PermissionTypeByIdDto, IRequest<PermissionTypeDto> { }

    public class PermissionTypeGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<PermissionTypeGetByIdQuery, PermissionTypeDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<PermissionTypeDto> Handle(PermissionTypeGetByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.PermissionTypeRepository.FirstOrDefaultAsync(
                where: x =>
                x.Id == request.Id) ?? throw new NotFoundException(ErrorMessage.NotFoundEntityById.Replace("{entity}", "Tipo de permiso").Replace("{id}", request.Id.ToString()));
            var result = _mapper.Map<PermissionType, PermissionTypeDto>(data);

            return result;
        }
    }
}