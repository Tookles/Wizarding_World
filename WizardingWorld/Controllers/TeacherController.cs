﻿using Microsoft.AspNetCore.Mvc;
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


    }
}
