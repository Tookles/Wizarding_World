namespace WizardingWorld.Models.Entity
{
    public class Teacher
    {
        public int id { get; set; }
        public string name { get; set; }
        public string species { get; set; }
        public string personality { get; set; }
        public int rating { get; set; }
        public List<string> teaches { get; set; }
    }
}