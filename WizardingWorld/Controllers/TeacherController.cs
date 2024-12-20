using Microsoft.AspNetCore.Mvc;
using WizardingWorld.Models.Entity;
using WizardingWorld.Services;

namespace WizardingWorld.Controllers
{
    [Route("/api/teacher")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet]
        public IActionResult GetTeachers()
        {
            List<Teacher> teachers = _teacherService.GetAllTeachers();
            return Ok(teachers);
        }
        [HttpGet("{id}")]
        public IActionResult GetTeachersById(int id)
        {
            if (id < 1) return BadRequest();
            List<Teacher> teachers = _teacherService.GetTeacherById(id);
            if (teachers.Count == 0) return NotFound();
            return Ok(teachers);
        }
    }
}
