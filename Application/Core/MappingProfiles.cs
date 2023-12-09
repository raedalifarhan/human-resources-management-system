using Application.Branches;
using Application.Departments;
using Application.Employees;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Department, Department>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.DepartmentName))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId));

            CreateMap<Branch, BranchQueryDto>();
            CreateMap<BranchCommandDto, Branch>();

            CreateMap<Department, DepartmentQueryDto>()
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch!.BranchName));

            CreateMap<DepartmentCommandDto, Department>();

            CreateMap<Employee, EmployeeQueryDto>();
            CreateMap<EmployeeCommandDto, Employee>();
        }
    }
}
