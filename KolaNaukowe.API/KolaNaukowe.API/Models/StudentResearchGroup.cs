﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.API.Models
{
    public class StudentResearchGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        //public IEnumerable<Student> Students { get; set; }

        public StudentResearchGroup()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
