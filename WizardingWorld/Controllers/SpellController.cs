using Microsoft.AspNetCore.Mvc;
using WizardingWorld.Services;
using WizardingWorld.Models.Entity;

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
    }
}
