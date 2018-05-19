using AutoMapper;
using KolaNaukowe.web.Dtos;
using KolaNaukowe.web.Extensions;
using KolaNaukowe.web.Models;
using KolaNaukowe.web.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KolaNaukowe.web.Services
{
    public class StudentResearchGroupService : IStudentResearchGroupService
    {

        private readonly IGenericRepository<StudentResearchGroup> _genericRepository;
        private readonly IMapper _mapper;


        public StudentResearchGroupService(IGenericRepository<StudentResearchGroup> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public StudentResearchGroupDto Add(StudentResearchGroup newStudentResearchGroup)
        {
            var studentResearchGroup = _genericRepository.Add(newStudentResearchGroup);
            return _mapper.Map<StudentResearchGroup, StudentResearchGroupDto>(studentResearchGroup);
        }

       
        public StudentResearchGroupDto Get(int id)
        {
            var studentResearchGroup = _genericRepository.Get(g => g.Id.Equals(id), g => g.Subjects);
            return _mapper.Map<StudentResearchGroup, StudentResearchGroupDto>(studentResearchGroup);
        }

        public IEnumerable<StudentResearchGroupDto> GetAll()
        {
            var studentResearchGroups = _genericRepository.GetAll(g => g.Subjects)
                                                          .OrderBy(c => c.Name);
                                                                                                                                     
            return _mapper.Map<IEnumerable<StudentResearchGroup>, IEnumerable<StudentResearchGroupDto>>(studentResearchGroups);                      
        }

        public IEnumerable<string> GetAllSubjects()
        {
            var subjects = from l in _genericRepository.GetAll()
                           from s in l.Subjects
                           select s.Name;
            return subjects.ToList();
        }


        public IEnumerable<StudentResearchGroupDto> FilterByName(IEnumerable<StudentResearchGroupDto> source, string searchString)
        {
            var filteringResult = source.Where(s => s.Name.Contains(searchString, System.StringComparison.CurrentCultureIgnoreCase));
            return filteringResult;
        }

        public IEnumerable<StudentResearchGroupDto> FilterBySubject(IEnumerable<StudentResearchGroupDto> source, string searchString)
        {
            var filteringResult = source.Where(s => s.Subjects.Any(u => u.Name == searchString));
            return filteringResult;
        }

        public void Remove(int id)
        {
            _genericRepository.Remove(id);
        }

        public void Update(StudentResearchGroup item)
        {
            _genericRepository.Update(item);
        }

        public IEnumerable<WriteStudentResearchGroupDto> WriteAll()
        {
            var studentResearchGroups = _genericRepository.GetAll(g => g.Subjects)
                                                          .OrderBy(c => c.Name);

            return _mapper.Map<IEnumerable<StudentResearchGroup>, IEnumerable<WriteStudentResearchGroupDto>>(studentResearchGroups);
        }

        public WriteDetailsStudentResearchGroupDto WriteDetails(int id)
        {
            var studentResearchGroup = _genericRepository.Get(g => g.Id.Equals(id), g => g.Subjects);
            return _mapper.Map<StudentResearchGroup, WriteDetailsStudentResearchGroupDto>(studentResearchGroup);
        }
    }
}
