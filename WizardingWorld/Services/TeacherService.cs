using WizardingWorld.Models.Entity;
using WizardingWorld.Models;

namespace WizardingWorld.Services
{
    public interface ITeacherService
    {
        List<Teacher> GetAllTeachers();
        List<Teacher> GetTeacherById(int id);

        Boolean AddTeacher(Teacher teacher); 
    }
    public class TeacherService : ITeacherService
    {

        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public List<Teacher> GetAllTeachers()
        {
            return _teacherRepository.FetchAllTeachers();
        }
        public List<Teacher> GetTeacherById(int id)
        {
            return _teacherRepository.FetchTeacherById(id);
        }

        public Boolean AddTeacher(Teacher teacher)
        {
            return _teacherRepository.AddTeacher(teacher);
        }

    }
}
