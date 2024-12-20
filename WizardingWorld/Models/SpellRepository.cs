using System.Text.Json;
using WizardingWorld.Models.Entity; 

namespace WizardingWorld.Models
{
    public interface ISpellRepository
    {
        List<Spell> FetchAllSpells(); 
    }

    public class SpellRepository : ISpellRepository
    {
        string spellPath = "Resources\\Spells.json";

        public List<Spell> FetchAllSpells()
        {
            List<Spell> allSpells = JsonSerializer.Deserialize<List<Spell>>(File.ReadAllText(spellPath));
            return allSpells;

        }
    }
}
