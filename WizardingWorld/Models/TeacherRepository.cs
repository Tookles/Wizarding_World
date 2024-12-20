using System.Text.Json;
using WizardingWorld.Models.Entity;

namespace WizardingWorld.Models
{
    public interface ITeacherRepository
    {
        List<Teacher> FetchAllTeachers();
        List<Teacher> FetchTeacherById(int id);
        Boolean AddTeacher(Teacher teacher);
        Boolean ExistById(int id); 

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


        public Boolean ExistById(int id) {
            List<Teacher> allTeachers = JsonSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            return allTeachers.Where(t => t.id == id).Any();
        }


        public Boolean AddTeacher(Teacher teacher)
        {
            List<Teacher> allTeachers = JsonSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            teacher.id = allTeachers.Last().id + 1; 
            allTeachers.Add(teacher);
            File.WriteAllText(teacherPath, JsonSerializer.Serialize(allTeachers, new JsonSerializerOptions() { WriteIndented = true }));
            return ExistById(teacher.id);
        }

    }
}
