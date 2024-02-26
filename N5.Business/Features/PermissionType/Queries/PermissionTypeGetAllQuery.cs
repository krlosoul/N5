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

    public class PermissionTypeGetAllQuery : IRequest<IEnumerable<PermissionTypeDto>> { }

    public class PermissionTypeGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<PermissionTypeGetAllQuery, IEnumerable<PermissionTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<PermissionTypeDto>> Handle(PermissionTypeGetAllQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.PermissionTypeRepository.GetAllAsync() ?? throw new NotFoundException(ErrorMessage.NotFoundEntity.Replace("{entity}", "Tipo de permiso"));
            var result = _mapper.Map<IEnumerable<PermissionType>, IEnumerable<PermissionTypeDto>>(data);

            return result;
        }
    }
}