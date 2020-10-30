using AutoMapper;
using $ext_safeprojectname$.Application.Application.Models;
using $ext_safeprojectname$.Application.Dtos;
using $ext_safeprojectname$.Data;


namespace $safeprojectname$.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Applications, ApplicationsDTO>();
        }
    }
}