namespace WizardingWorld.Models.Entity
{
    public class TeacherSpells
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int SpellId { get; set; }
        public Spell Spell { get; set; }
    }
}
