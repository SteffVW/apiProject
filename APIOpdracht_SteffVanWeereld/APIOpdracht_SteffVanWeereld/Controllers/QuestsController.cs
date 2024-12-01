using APIOpdracht_SteffVanWeereld.Models;
using APIOpdracht_SteffVanWeereld.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIOpdracht_SteffVanWeereld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestsController : ControllerBase
    {
        private readonly IQuestsService _questsService;

        public QuestsController(IQuestsService questsService)
        {
            _questsService = questsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Quest>>> GetAll()
        {
            var allQuests = await _questsService.GetAllQuests();
            return Ok(allQuests);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Quest>> GetById(int id)
        {
            var questToGet = await _questsService.GetQuestById(id);

            if (questToGet != null)
            {
                return Ok(questToGet);
            }

            return NotFound(questToGet);
        }
        [HttpGet("{id}/image")]
        public async Task<ActionResult<Quest>> GetImage(int id)
        {
            var quest = await _questsService.GetQuestById(id);

            if (quest != null)
            {
                return Ok(new {
                    name = quest.Name,
                    image = quest.Image
                });
            }

            return NotFound(quest);
        }
        [HttpGet("difficulty/{difficulty}")]
        public async Task<ActionResult<Quest>> GetByDifficulty(string difficulty)
        {
            var questsToGet = await _questsService.GetQuestsByDifficulty(difficulty);

            if (questsToGet != null)
            {
                return Ok(questsToGet);
            }

            return NotFound(questsToGet);
        }
        [HttpGet("region/{regionId}")]
        public async Task<ActionResult<Quest>> GetByRegion(int regionId)
        {
            var questsToGet = await _questsService.GetQuestsByRegion(regionId);

            if (questsToGet != null)
            {
                return Ok(questsToGet);
            }

            return NotFound(questsToGet);
        }
        [HttpGet("boss/{bossId}")]
        public async Task<ActionResult<Quest>> GetByBoss(int bossId)
        {
            var questToGet = await _questsService.GetQuestById(bossId);

            if (questToGet != null)
            {
                return Ok(questToGet);
            }

            return NotFound(questToGet);
        }
        [HttpPost]
        public async Task<ActionResult<Quest?>> PostQuest(Quest quest)
        {
            await _questsService.CreateQuest(quest);
            return CreatedAtAction(nameof(GetById), new { Id = quest.Id }, quest);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuest(int id, Quest updatedQuest)
        {
            if (id != updatedQuest.Id)
            {
                return BadRequest("Quest ID mismatch.");
            }

            var existingQuest = await _questsService.GetQuestById(id);
            if (existingQuest == null)
            {
                return NotFound();
            }

            await _questsService.UpdateQuest(updatedQuest);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuest(int id)
        {
            var existingQuest = await _questsService.GetQuestById(id);
            if (existingQuest == null)
            {
                return NotFound();
            }

            await _questsService.DeleteQuest(id);
            return NoContent();
        }
    }
}
