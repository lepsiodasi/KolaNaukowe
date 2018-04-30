﻿using KolaNaukowe.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolaNaukowe.web.Dtos
{
    public class StudentResearchGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Department { get; set; }
        public List<Student> Students { get; set; }
        public string OwnerId { get; set; }
    }
}