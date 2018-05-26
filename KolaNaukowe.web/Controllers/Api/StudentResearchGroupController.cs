﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KolaNaukowe.web.Data;
using KolaNaukowe.web.Dtos;
using KolaNaukowe.web.Models;
using KolaNaukowe.web.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KolaNaukowe.web.Controllers.Api
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/StudentResearchGroup")]
    public class StudentResearchGroupController : Controller
    {
        private IStudentResearchGroupService _studentResearchGroupService;
        private KolaNaukoweDbContext _context;

        public StudentResearchGroupController(IStudentResearchGroupService studentResearchGroupService, KolaNaukoweDbContext context)
        {
            _context = context;
            _studentResearchGroupService = studentResearchGroupService;
        }

        [HttpGet("Test")]
        public IActionResult TestText()
        {
            return Json("test text");
        }

        [HttpGet("WriteAll")]
        public IActionResult WriteAll()
        {
            var model = _studentResearchGroupService.WriteAll();
            return Json(model);
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var model = _studentResearchGroupService.GetAll();
            return Json(model);
        }
        
        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var student = _studentResearchGroupService.WriteDetails(id);
            if(student == null)
                return NotFound(id);
            return Json(student);
        }

        [HttpDelete("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var group = _studentResearchGroupService.Get(id);
                if (group == null)
                {
                    return NotFound();
                }

                _studentResearchGroupService.Remove(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] StudentResearchGroup studentGroup)
        {
            try
            {
                var group = _studentResearchGroupService.Get(id);
                if (group == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                group.Name = studentGroup.Name;
                // to fill 
                _studentResearchGroupService.Update(studentGroup);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public void Insert([FromBody] StudentResearchGroup newStudentResearchGroup)
        {
            _studentResearchGroupService.Add(newStudentResearchGroup);
        }
    }
}