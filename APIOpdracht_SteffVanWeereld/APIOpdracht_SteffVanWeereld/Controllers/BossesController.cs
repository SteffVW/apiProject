using APIOpdracht_SteffVanWeereld.Models;
using APIOpdracht_SteffVanWeereld.Services;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIOpdracht_SteffVanWeereld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BossesController : ControllerBase
    {
        private readonly IBossesService _bossesService;

        public BossesController(IBossesService bossesService)
        {
            _bossesService = bossesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Boss>>> GetAll()
        {
            var allBosses = await _bossesService.GetAllBosses();
            return Ok(allBosses);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Boss>> GetById(int id)
        {
            var bossToGet = await _bossesService.GetBossById(id);

            if (bossToGet != null)
            {
                return Ok(bossToGet);
            }

            return NotFound(bossToGet);
        }
        [HttpGet("{id}/image")]
        public async Task<ActionResult<Boss>> GetImage(int id)
        {
            var boss = await _bossesService.GetBossById(id);

            if (boss != null)
            {
                return Ok(new {
                    name = boss.Name,
                    image = boss.Image
                });
            }

            return NotFound(boss);
        }
        [HttpGet("region/{regionId}")]
        public async Task<ActionResult<List<Boss>>> GetByRegion(int regionId)
        {
            var bossesInRegion = await _bossesService.GetBossesByRegion(regionId);
            if (bossesInRegion != null)
            {
                return Ok(bossesInRegion);
            }
            return NotFound();
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<Boss>>> SearchByName([FromQuery] string name)
        {
            var matchingBosses = await _bossesService.SearchBossesByName(name);
            if (matchingBosses != null)
            {
                return Ok(matchingBosses);
            }
            return NotFound();
        }
        [HttpGet("maxHit/{count}")]
        public async Task<ActionResult<List<Boss>>> GetTopBosses(int count)
        {
            var topBosses = await _bossesService.GetTopBossesByMaxHit(count);
            return Ok(topBosses);
        }
        [HttpPost]
        public async Task<ActionResult<Boss?>> PostBoss(Boss boss)
        {
            await _bossesService.CreateBoss(boss);
            return CreatedAtAction(nameof(GetById), new { Id = boss.Id }, boss);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBoss(int id, Boss updatedBoss)
        {
            if (id != updatedBoss.Id)
            {
                return BadRequest("Boss ID mismatch.");
            }

            var existingBoss = await _bossesService.GetBossById(id);
            if (existingBoss == null)
            {
                return NotFound();
            }

            await _bossesService.UpdateBoss(updatedBoss);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBoss(int id)
        {
            var existingBoss = await _bossesService.GetBossById(id);
            if (existingBoss == null)
            {
                return NotFound();
            }

            await _bossesService.DeleteBoss(id);
            return NoContent();
        }
    }
}
