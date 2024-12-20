using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WizardingWorld.Models.Entity;
using WizardingWorld.Services;

namespace WizardingWorld.Controllers
{
    [Route("/api/{controller}")]
    [ApiController]
    public class TeacherController : Controller
    {

        private readonly IMemoryCache _cache;

        private const string TeacherCacheKey = "TeacherList"; 


        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService, IMemoryCache memoryCache)
        {
            _teacherService = teacherService;
            _cache = memoryCache;
        }


        [HttpGet]
        public IActionResult GetTeachers()
        {
            List<Teacher> teachers; 

            if (!_cache.TryGetValue(TeacherCacheKey, out teachers))
            {
                teachers = _teacherService.GetAllTeachers();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                _cache.Set(TeacherCacheKey, teachers, cacheEntryOptions);
            }

            return Ok(teachers);
        }



        [HttpGet("{id}")]
        public IActionResult GetTeachersById(int id)
        {
            if (id < 1) return BadRequest("Please enter a positive integer");
            List<Teacher> teachers = _teacherService.GetTeacherById(id);
            if (teachers.Count == 0) return NotFound($"Unable to find a teacher with id: {id}");
            return Ok(teachers);
        }


        [HttpPost]
        public IActionResult AddTeacher([FromBody] Teacher teacher)
        {
            if (teacher.name == "")
            {
                return BadRequest();
            }

            Boolean IsSuccess = _teacherService.AddTeacher(teacher);
            
            if (IsSuccess)
            {
                return Created("/api/teacher", teacher);
            } else
            {
                return StatusCode(500); 
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeacherById(int id)
        {
            if (id < 1) return BadRequest("Please enter a positive integer");

            if (!_teacherService.ExistById(id)) return NotFound("No teacher exists with this Id");

            _teacherService.DeleteTeacherById(id);

            if (_teacherService.ExistById(id))
            {
                return StatusCode(500);
            }
            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult UpdateTeacherById(int id, [FromBody] JsonPatchDocument<Teacher> doc)
        {
            if (doc != null)
            {
                Teacher teacher = _teacherService.GetTeacherById(id).First();
                doc.ApplyTo(teacher, ModelState);
                if (!ModelState.IsValid) return BadRequest();
                _teacherService.UpdateTeacher(teacher);
                return Ok(teacher);
            }
            return BadRequest();
        }
        [HttpPatch]
        public IActionResult UpdateTeacher([FromBody] JsonPatchDocument<Teacher> doc)
        {
            if (doc != null)
            {
                Teacher teacher = new();
                doc.ApplyTo(teacher, ModelState);
                if (!ModelState.IsValid) return BadRequest();
                _teacherService.UpdateTeacher(teacher);
                return Ok(teacher);
            }
            return BadRequest();
        }
    }
}
