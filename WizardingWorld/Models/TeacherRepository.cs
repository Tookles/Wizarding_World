using System.Text.Json;
using WizardingWorld.Models.Entity;

namespace WizardingWorld.Models
{
    public interface ITeacherRepository
    {
        List<Teacher> FetchAllTeachers();
        List<Teacher> FetchTeacherById(int id);
    }
    public class TeacherRepository : ITeacherRepository
    {
        string teacherPath = "Resources\\Teachers.json";

        public List<Teacher> FetchAllTeachers()
        {
            List<Teacher> allTeachers = JsonSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            return allTeachers;
        }
        public List<Teacher> FetchTeacherById(int id)
        {
            List<Teacher> allTeachers = JsonSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            return allTeachers.Where(teacher => teacher.id == id).ToList();
        }
    }
}
