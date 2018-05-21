using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KolaNaukowe.web.Models;
using KolaNaukowe.web.Data;
using Microsoft.AspNetCore.Authorization;
using KolaNaukowe.web.Services;
using System.Linq;
using KolaNaukowe.web.Extensions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KolaNaukowe.web.Controllers
{
    public class HomeController : Controller
    {
        private IStudentResearchGroupService _studentResearchGroupService;
        private ISubjectService _subjectService;
        private KolaNaukoweDbContext _context;

        public HomeController(IStudentResearchGroupService studentResearchGroupService, ISubjectService subjectService, KolaNaukoweDbContext context)
        {
            _context = context;
            _studentResearchGroupService = studentResearchGroupService;
            _subjectService = subjectService;
        }
        
        public IActionResult Index(string searchName, string researchGroupSubject)
        {
            var model = _studentResearchGroupService.GetAll();

            if (!string.IsNullOrEmpty(searchName))
            {
                model = _studentResearchGroupService.FilterByName(model, searchName);
            }

            if (!string.IsNullOrEmpty(researchGroupSubject))
            {
                model = _studentResearchGroupService.FilterBySubject(model, researchGroupSubject);
            }
            var researchGroupVM = new ResearchGroupViewModel();
            researchGroupVM.subjects = new SelectList( _studentResearchGroupService.GetAllSubjects().Distinct().ToList());
            researchGroupVM.researchGroups = model.ToList();

            return View(researchGroupVM);
        }

        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentResearchGroupService.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [Authorize(Policy = "LeaderAndAdmin")]
        public IActionResult Create()
        {    
            return View();
        }


        [HttpPost]
        public IActionResult Create(StudentResearchGroup newStudentResearchGroup)
        {
            if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
            var studentResearchGroup = _studentResearchGroupService.Add(newStudentResearchGroup);
            return View(studentResearchGroup); 
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            var studentResearchGroup = _studentResearchGroupService.Get(id);

            var subjects = _context.Subjects.Where(s => s.researchGroups.Id == studentResearchGroup.Id).ToList();

            foreach(var subject in subjects)
            {
                _subjectService.Remove(subject.Id);
            }

            _studentResearchGroupService.Remove(id);

            return View(studentResearchGroup);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete()
        {
            return View();
        }
        [Authorize(Policy = "LeaderAndAdmin")]
        public IActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Edit(int id, StudentResearchGroup studentGroup)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _studentResearchGroupService.Update(studentGroup);
            var studentResearchGroup = _studentResearchGroupService.Get(id);
            return View(studentResearchGroup);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }
        public IActionResult Contact()
        {
      
            ViewData["Message"] = "Contact";
            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
