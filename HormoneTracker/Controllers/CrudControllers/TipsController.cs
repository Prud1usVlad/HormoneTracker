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
    public class TipsController : ControllerBase
    {
        private readonly HormoneTrackerDBContext _context;

        public TipsController(HormoneTrackerDBContext context)
        {
            _context = context;
        }

        // GET: api/Tips
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Tip>>> GetTips()
        {
            return await _context.Tips.ToListAsync();
        }

        // GET: api/Tips/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Tip>> GetTip(int id)
        {
            var tip = await _context.Tips.FindAsync(id);

            if (tip == null)
            {
                return NotFound();
            }

            return tip;
        }

        // PUT: api/Tips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTip(int id, Tip tip)
        {
            if (id != tip.TipId)
            {
                return BadRequest();
            }

            _context.Entry(tip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipExists(id))
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

        // POST: api/Tips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Tip>> PostTip(Tip tip)
        {
            _context.Tips.Add(tip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTip", new { id = tip.TipId }, tip);
        }

        // DELETE: api/Tips/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTip(int id)
        {
            var tip = await _context.Tips.FindAsync(id);
            if (tip == null)
            {
                return NotFound();
            }

            _context.Tips.Remove(tip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipExists(int id)
        {
            return _context.Tips.Any(e => e.TipId == id);
        }
    }
}
