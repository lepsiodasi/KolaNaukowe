using KolaNaukowe.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.web.Dtos
{
    public class StudentResearchGroupDto
    {
        public StudentResearchGroupDto()
        {
            Subjects = new List<Subject>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set; }
        public string Department { get; set; }
        public string Leader { get; set; }
        public string Attendant { get; set; }
        public string Description { get; set; }
    }
}
