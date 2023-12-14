using AutoMapper;
using HR.API.DTOs.EmployeeDTOs;

namespace HR.API.MappingProfile
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            // Source => Target
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<GetEmployeeDTO, Employee>();
            CreateMap<AddressDTO, Address>();
            CreateMap<GetEmployeeDTO, PublishEmployeeDTO>();
        }
    }
}
