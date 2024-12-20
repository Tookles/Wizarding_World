using systemSerializer = System.Text.Json.JsonSerializer;
using WizardingWorld.Models.Entity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;


namespace WizardingWorld.Models
{
    public interface ITeacherRepository
    {
        List<Teacher> FetchAllTeachers();
        List<Teacher> FetchTeacherById(int id);
        Boolean AddTeacher(Teacher teacher);
        Boolean ExistById(int id);

        void DeleteTeacherById(int id);
        void UpdateTeacher(Teacher teacher);

    }
    public class TeacherRepository : ITeacherRepository
    {
        string teacherPath = "Resources\\Teachers.json";

        public List<Teacher> FetchAllTeachers()
        {
            List<Teacher> allTeachers = systemSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            return allTeachers;
        }
        public List<Teacher> FetchTeacherById(int id)
        {
            List<Teacher> allTeachers = systemSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            return allTeachers.Where(teacher => teacher.id == id).ToList();
        }


        public Boolean ExistById(int id)
        {
            List<Teacher> allTeachers = systemSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            return allTeachers.Where(t => t.id == id).Any();
        }


        public Boolean AddTeacher(Teacher teacher)
        {
            List<Teacher> allTeachers = systemSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            teacher.id = allTeachers.Last().id + 1;
            allTeachers.Add(teacher);
            File.WriteAllText(teacherPath, systemSerializer.Serialize(allTeachers));
            return ExistById(teacher.id);
        }

        public void DeleteTeacherById(int id)
        {
            List<Teacher> allTeachers = systemSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            Teacher teacherToDelete = allTeachers.Where(t => t.id == id).First();
            allTeachers.Remove(teacherToDelete);
            File.WriteAllText(teacherPath, systemSerializer.Serialize(allTeachers));
        }

        public void UpdateTeacher(Teacher teacher)
        {
            List<Teacher> allTeachers = systemSerializer.Deserialize<List<Teacher>>(File.ReadAllText(teacherPath));
            int indexToUpdate = allTeachers.IndexOf(allTeachers.First(t => t.id == teacher.id));
            allTeachers[indexToUpdate] = teacher;
            File.WriteAllText(teacherPath, systemSerializer.Serialize(allTeachers));
        }
    }
}