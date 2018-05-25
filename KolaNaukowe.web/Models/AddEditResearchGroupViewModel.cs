using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.web.Models
{
    public class AddEditResearchGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Subject Subject1 { get; set; }
        public Subject Subject2 { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Department { get; set; }
        public string Leader { get; set; }
        public string Attendant { get; set; }
        public string Description { get; set; }
    }
}
