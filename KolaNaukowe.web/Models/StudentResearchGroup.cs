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
        [Key]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Department { get; set; }
        public bool ApprovedByAdmin { get; set; } = false;
        public IEnumerable<Student> Students { get; set; }

        // user ID from AspNetUser table.
        public string OwnerId { get; set; }

    }
}
