using WizardingWorld.Models.Entity;
using WizardingWorld.Models;

namespace WizardingWorld.Services
{
    public interface ITeacherService
    {
        List<Teacher> GetAllTeachers();
        List<Teacher> GetTeacherById(int id);
        bool AddTeacher(Teacher teacher);
        void DeleteTeacherById(int id);
        bool ExistById(int id);
        void UpdateTeacher(Teacher teacher);
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
        public void DeleteTeacherById(int id)
        {
             _teacherRepository.DeleteTeacherById(id); 
        }
        public Boolean ExistById(int id)
        {
            return _teacherRepository.ExistById(id);
        }
        public void UpdateTeacher(Teacher teacher)
        {
            _teacherRepository.UpdateTeacher(teacher);
        }
    }
}
