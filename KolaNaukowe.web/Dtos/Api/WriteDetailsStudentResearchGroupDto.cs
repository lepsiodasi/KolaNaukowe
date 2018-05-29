using KolaNaukowe.web.Dtos.Api;
using KolaNaukowe.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.web.Dtos
{
    public class WriteDetailsStudentResearchGroupDto
    {
        public WriteDetailsStudentResearchGroupDto()
        {
            Subjects = new List<SubjectDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Department { get; set; }
        public string Attendant { get; set; }
        public string Leader { get; set; }
        public string Description { get; set; }
        public List<SubjectDto> Subjects { get; set; }
    }
}
