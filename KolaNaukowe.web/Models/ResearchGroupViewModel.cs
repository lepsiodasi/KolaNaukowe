using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.web.Models
{
    public class ResearchGroupViewModel
    {
        public IEnumerable<KolaNaukowe.web.Dtos.StudentResearchGroupDto> researchGroups;
        public SelectList subjects; 
        public string researchGroupSubject { get; set; }
    }
}
