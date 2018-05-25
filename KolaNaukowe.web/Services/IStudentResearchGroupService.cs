using KolaNaukowe.web.Dtos;
using KolaNaukowe.web.Dtos.Api;
using KolaNaukowe.web.Models;
using System.Collections.Generic;

namespace KolaNaukowe.web.Services
{
    public interface IStudentResearchGroupService
    {
        IEnumerable<StudentResearchGroupDto> GetAll();
        IEnumerable<WriteStudentResearchGroupDto> WriteAll();
        WriteDetailsStudentResearchGroupDto WriteDetails(int id);
        IEnumerable<string> GetAllSubjects();
        StudentResearchGroupDto Get(int id);
        StudentResearchGroupDto Add(StudentResearchGroup newStudentResearchGroup);
        void Add(AddEditStudentResearchGroupDto studentResearchGroup);
        bool Update(int id, AddEditStudentResearchGroupDto studentResearchGroup);
        void Update(StudentResearchGroup item);
        void Remove(int id);
        IEnumerable<StudentResearchGroupDto> FilterByName(IEnumerable<StudentResearchGroupDto> source, string searchString);
        IEnumerable<StudentResearchGroupDto> FilterBySubject(IEnumerable<StudentResearchGroupDto> source, string searchString);
    }
}
