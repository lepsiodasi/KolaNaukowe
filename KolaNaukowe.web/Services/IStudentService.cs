using KolaNaukowe.web.Dtos;
using KolaNaukowe.web.Models;
using System.Collections.Generic;

namespace KolaNaukowe.web.Services
{
    public interface IStudentService
    {
        IEnumerable<StudentDto> GetAll();
        StudentDto Get(int id);
        StudentDto Add(Student item);
        void Update(Student item);
        void Remove(int id);
        
    }
}
