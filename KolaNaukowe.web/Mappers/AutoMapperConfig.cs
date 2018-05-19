using AutoMapper;
using KolaNaukowe.web.Dtos;
using KolaNaukowe.web.Models;
using System.Collections.Generic;

namespace KolaNaukowe.web.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize() 
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>();
                cfg.CreateMap<StudentResearchGroup, StudentResearchGroupDto>();
                cfg.CreateMap<StudentResearchGroup, WriteStudentResearchGroupDto>();
                cfg.CreateMap<StudentResearchGroup, WriteDetailsStudentResearchGroupDto>();
            }).CreateMapper();
    }
}
