using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.web.Models
{
    public class StudentResearchGroup
    {
        public StudentResearchGroup()
        {
            Subjects = new List<Subject>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Department { get; set; }
        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set; }
        public string Leader { get; set; }
        public string Attendant { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
    }
}
