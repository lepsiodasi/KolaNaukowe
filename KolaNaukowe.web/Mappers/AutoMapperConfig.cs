using AutoMapper;
using KolaNaukowe.web.Dtos;
using KolaNaukowe.web.Dtos.Api;
using KolaNaukowe.web.Models;
using System.Collections.Generic;

namespace KolaNaukowe.web.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize() 
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StudentResearchGroup, StudentResearchGroupDto>();
                cfg.CreateMap<StudentResearchGroup, WriteStudentResearchGroupDto>();
                cfg.CreateMap<StudentResearchGroup, WriteDetailsStudentResearchGroupDto>();
                cfg.CreateMap<Subject, SubjectDto>();
            }).CreateMapper();
    }
}
