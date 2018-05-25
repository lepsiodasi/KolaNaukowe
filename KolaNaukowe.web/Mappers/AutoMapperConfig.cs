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
                cfg.CreateMap<StudentResearchGroupDto, StudentResearchGroup>();
                cfg.CreateMap<StudentResearchGroupDto, AddEditResearchGroupViewModel>();
                cfg.CreateMap<StudentResearchGroup, WriteStudentResearchGroupDto>();
                cfg.CreateMap<StudentResearchGroup, AddEditStudentResearchGroupDto>();
                cfg.CreateMap<StudentResearchGroup, WriteDetailsStudentResearchGroupDto>();
                cfg.CreateMap<AddEditResearchGroupViewModel, StudentResearchGroup>();
                cfg.CreateMap<AddEditResearchGroupViewModel, StudentResearchGroupDto>();
                cfg.CreateMap<Subject, SubjectDto>();
                cfg.CreateMap<SubjectDto, Subject>();

            }).CreateMapper();
    }
}
