﻿using KolaNaukowe.web.Dtos;
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
        IEnumerable<WriteStudentResearchGroupDto> WriteAll();
        WriteDetailsStudentResearchGroupDto WriteDetails(int id);
        IEnumerable<string> GetAllSubjects();
        StudentResearchGroupDto Get(int id);
        StudentResearchGroupDto Add(StudentResearchGroup newStudentResearchGroup);
        void Update(StudentResearchGroup item);
        void Remove(int id);
        IEnumerable<StudentResearchGroupDto> FilterByName(IEnumerable<StudentResearchGroupDto> source, string searchString);
        IEnumerable<StudentResearchGroupDto> FilterBySubject(IEnumerable<StudentResearchGroupDto> source, string searchString);
    }
}
