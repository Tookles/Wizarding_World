using Microsoft.AspNetCore.Mvc;
using WizardingWorld.Services;
using WizardingWorld.Models.Entity;
using Microsoft.AspNetCore.RateLimiting;

namespace WizardingWorld.Controllers
{
    [Route("/api/spells")]
    [ApiController]   
    public class SpellController : Controller
    {
        private readonly ISpellService _spellService;
        public SpellController(ISpellService spellService)
        {
            _spellService = spellService;
        }
        [HttpGet]
        public IActionResult GetSpells()
        {
            List<Spell> spells = _spellService.GetAllSpells();
            return Ok(spells);
        }
        [HttpGet("random")]
        [EnableRateLimiting("fixed")]
        public IActionResult GetRandomSpell()
        {
            Spell spell = _spellService.GetRandomSpell();
            if (spell == null) return NotFound("No spells found");
            return Ok(spell);
        }
    }
}
