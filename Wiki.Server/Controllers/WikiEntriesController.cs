#region usings

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wiki.Server.Models;
using Wiki.Shared;

#endregion

namespace Wiki.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikiEntriesController : ControllerBase
    {
        private readonly Model _context;

        public WikiEntriesController(Model context)
        {
            _context = context;
        }

        // GET: api/WikiEntries
        [HttpGet]
        public IEnumerable<WikiEntry> GetWikiEntries()
        {
            return _context.WikiEntries;
        }

        [HttpGet("[action]/{limit}")]
        public IEnumerable<WikiEntry> GetWikiEntriesLimited([FromRoute] int limit)
        {
            return _context.WikiEntries.Take(limit);
        }

        [HttpGet("[action]")]
        public WikiEntry GetLastEntry()
        {
            return _context.WikiEntries.Last();
        }

        [HttpGet("[action]/{search}")]
        public IEnumerable<WikiEntry> Search([FromRoute] string search)
        {
            return _context.WikiEntries.Where(entry =>
                (entry.Title.ToLower().Contains(search.ToLower()) || entry.Tags.ToLower().Contains(search.ToLower())));
        }

        // GET: api/WikiEntries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWikiEntry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wikiEntry = await _context.WikiEntries.FindAsync(id).ConfigureAwait(false);

            if (wikiEntry == null)
            {
                return NotFound();
            }

            return Ok(wikiEntry);
        }

        // PUT: api/WikiEntries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWikiEntry([FromRoute] int id, [FromBody] WikiEntry wikiEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wikiEntry.ID)
            {
                return BadRequest();
            }

            _context.Entry(wikiEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WikiEntryExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/WikiEntries
        [HttpPost]
        public async Task<IActionResult> PostWikiEntry([FromBody] WikiEntry wikiEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WikiEntries.Add(wikiEntry);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetWikiEntry", new {id = wikiEntry.ID}, wikiEntry);
        }

        // DELETE: api/WikiEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWikiEntry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wikiEntry = await _context.WikiEntries.FindAsync(id).ConfigureAwait(false);
            if (wikiEntry == null)
            {
                return NotFound();
            }

            _context.WikiEntries.Remove(wikiEntry);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(wikiEntry);
        }

        private bool WikiEntryExists(int id)
        {
            return _context.WikiEntries.Any(e => e.ID == id);
        }
    }
}