using WizardingWorld.Models;
using WizardingWorld.Models.Entity;


namespace WizardingWorld.Services
{
    public interface ISpellService
    {
        List<Spell> GetAllSpells();
        Spell GetRandomSpell();
    }
    public class SpellService : ISpellService
    {
        private readonly ISpellRepository _spellRepository;
        public SpellService(ISpellRepository spellRepository)
        {
            _spellRepository = spellRepository;
        }
        public List<Spell> GetAllSpells()
        {
           return _spellRepository.FetchAllSpells();
        }
        public Spell GetRandomSpell()
        {
            return _spellRepository.FetchRandomSpell();
        }
    }
}
