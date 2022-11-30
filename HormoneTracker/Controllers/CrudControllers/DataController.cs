using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HormoneTracker.Core.Models;
using HormoneTracker.DAL;
using Microsoft.AspNetCore.Authorization;

namespace HormoneTracker.Controllers.CrudControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly HormoneTrackerDBContext _context;

        public DataController(HormoneTrackerDBContext context)
        {
            _context = context;
        }

        // GET: api/Data
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Datum>>> GetData()
        {
            return await _context.Data.ToListAsync();
        }

        // GET: api/Data/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Datum>> GetDatum(int id)
        {
            var datum = await _context.Data.FindAsync(id);

            if (datum == null)
            {
                return NotFound();
            }

            return datum;
        }

        // PUT: api/Data/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDatum(int id, Datum datum)
        {
            if (id != datum.DataId)
            {
                return BadRequest();
            }

            _context.Entry(datum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Data
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Datum>> PostDatum(Datum datum)
        {
            _context.Data.Add(datum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DatumExists(datum.DataId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDatum", new { id = datum.DataId }, datum);
        }

        // DELETE: api/Data/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatum(int id)
        {
            var datum = await _context.Data.FindAsync(id);
            if (datum == null)
            {
                return NotFound();
            }

            _context.Data.Remove(datum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DatumExists(int id)
        {
            return _context.Data.Any(e => e.DataId == id);
        }
    }
}
