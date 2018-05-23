using KolaNaukowe.web.Dtos;
using KolaNaukowe.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.web.Services
{
    public interface IStudentResearchGroupService
    {
        IEnumerable<StudentResearchGroupDto> GetAll();
        IEnumerable<StudentResearchGroupDto> Filter(string searchString);
        StudentResearchGroupDto Get(int id);
        StudentResearchGroupDto Add(StudentResearchGroup newStudentResearchGroup);
        void Update(StudentResearchGroup item);
        void Remove(int id);
        void Accept(StudentResearchGroup item);

    }
}
