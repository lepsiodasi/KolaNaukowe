using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KolaNaukowe.web.Data;
using KolaNaukowe.web.Dtos;
using KolaNaukowe.web.Dtos.Api;
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
        private ISubjectService _subjectService;
        private KolaNaukoweDbContext _context;
        private IMapper _mapper;

        public StudentResearchGroupController(IStudentResearchGroupService studentResearchGroupService, ISubjectService subjectService, KolaNaukoweDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _studentResearchGroupService = studentResearchGroupService;
            _subjectService = subjectService;
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

        /*
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var model = _studentResearchGroupService.GetAll();
            return Json(model);
        }
        */

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
                _studentResearchGroupService.Remove(id);         
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }  
        }

        [HttpPost("Add")]
        public IActionResult Insert([FromBody] AddEditStudentResearchGroupDto studentResearchGroup)
        {


            _studentResearchGroupService.Add(studentResearchGroup);

            return Ok();

        }


        [HttpPut("Update/{id:int}")]
        public IActionResult Update(int id, [FromBody] AddEditStudentResearchGroupDto studentResearchGroup)
        {
 
            var result = _studentResearchGroupService.Update(id, studentResearchGroup);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}