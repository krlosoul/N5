namespace N5.Business.Commons.Mappers
{
    using AutoMapper;
    using N5.Core.Dtos.Permission;
    using N5.Core.Dtos.PermissionType;
    using N5.Core.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PermissionType, PermissionTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<PermissionTypeCreateDto, PermissionType>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Permission, PermissionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeForename, opt => opt.MapFrom(src => src.EmployeeForename))
                .ForMember(dest => dest.EmployeeSurname, opt => opt.MapFrom(src => src.EmployeeSurname))
                .ForMember(dest => dest.PermissionType, opt => opt.MapFrom(src => src.PermissionType))
                .ForMember(dest => dest.PermissionDate, opt => opt.MapFrom(src => src.PermissionDate));

            CreateMap<PermissionCreateDto, Permission>()
                .ForMember(dest => dest.EmployeeForename, opt => opt.MapFrom(src => src.EmployeeForename))
                .ForMember(dest => dest.EmployeeSurname, opt => opt.MapFrom(src => src.EmployeeSurname))
                .ForMember(dest => dest.PermissionType, opt => opt.MapFrom(src => src.PermissionType))
                .ForMember(dest => dest.PermissionDate, opt => opt.MapFrom(src => src.PermissionDate));
        }
    }
}