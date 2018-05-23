using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KolaNaukowe.web.Models;
using KolaNaukowe.web.Data;
using Microsoft.AspNetCore.Authorization;
using KolaNaukowe.web.Services;
using System.Linq;
using KolaNaukowe.web.Extensions;

namespace KolaNaukowe.web.Controllers
{
    public class HomeController : Controller
    {
        private IStudentResearchGroupService _studentResearchGroupService;
        private KolaNaukoweDbContext _context;

        public HomeController(IStudentResearchGroupService studentResearchGroupService, KolaNaukoweDbContext context)
        {
            _context = context;
            _studentResearchGroupService = studentResearchGroupService;
        }
        
        public IActionResult Index(string searchString)
        {
            var model = _studentResearchGroupService.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                model = _studentResearchGroupService.Filter(searchString);
            }
            return View(model.ToList());
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
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create()
        {    
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult ShowPending()
        {
            var model = _studentResearchGroupService.GetAll();

            
            return View(model.ToList());
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
        public IActionResult Delete(int id, StudentResearchGroup newStudentResearchGroup)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            var studentResearchGroup = _studentResearchGroupService.Get(id);
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

        [HttpPost]
        
        public IActionResult Accept(int id, StudentResearchGroup studentGroup)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(ShowPending));
            }
            _studentResearchGroupService.Accept(studentGroup);
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
