using System;
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
        
        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            var student = _studentResearchGroupService.Get(id);
            if(student == null)
                return NotFound(id);
            return Json(_studentResearchGroupService.GetAll().FirstOrDefault(x => x.Id == id));
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var studentResearchGroup = _studentResearchGroupService.Get(id);
            _studentResearchGroupService.Remove(id);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] StudentResearchGroup studentGroup)
        {
            _studentResearchGroupService.Update(studentGroup);
            var studentResearchGroup = _studentResearchGroupService.Get(id);
            if(studentResearchGroup != null)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        public void Insert([FromBody] StudentResearchGroup newStudentResearchGroup)
        {
            _studentResearchGroupService.Add(newStudentResearchGroup);
        }
    }
}