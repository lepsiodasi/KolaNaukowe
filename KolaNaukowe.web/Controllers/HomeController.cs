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
using IdentityServer4.AccessTokenValidation;
using AutoMapper;

namespace KolaNaukowe.web.Controllers
{
    public class HomeController : Controller
    {
        private IStudentResearchGroupService _studentResearchGroupService;
        private ISubjectService _subjectService;
        private KolaNaukoweDbContext _context;
        private IMapper _mapper;

        public HomeController(IStudentResearchGroupService studentResearchGroupService, ISubjectService subjectService, KolaNaukoweDbContext context, IMapper mapper)
        {
            _mapper = mapper;
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
            researchGroupVM.subjects = new SelectList(_studentResearchGroupService.GetAllSubjects().Distinct().ToList());
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
        [HttpGet]
        public IActionResult Create()
        {
            return View(new AddResearchGroupViewModel());
        }


        [HttpPost]
        public IActionResult Create(AddResearchGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            var researchGroup = new StudentResearchGroup();

            var newStudentResearchGroup = _mapper.Map<AddResearchGroupViewModel, StudentResearchGroup>(model);

            newStudentResearchGroup.Subjects.Add(model.Subject1);
            newStudentResearchGroup.Subjects.Add(model.Subject2);

            _studentResearchGroupService.Add(newStudentResearchGroup);

            return RedirectToAction("Index", "Home");
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

            foreach (var subject in subjects)
            {
                _subjectService.Remove(subject.Id);
            }

            _studentResearchGroupService.Remove(id);
            return RedirectToAction("Index", "Home");

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }


        [Authorize(Policy = "LeaderAndAdmin")]
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Edit(int id, AddResearchGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            var editStudentResearchGroup = _mapper.Map<AddResearchGroupViewModel, StudentResearchGroup>(model);
            _studentResearchGroupService.Update(editStudentResearchGroup);
            return RedirectToAction("Index", "Home");

        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
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
