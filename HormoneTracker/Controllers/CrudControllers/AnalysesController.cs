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
    public class AnalysesController : ControllerBase
    {
        private readonly HormoneTrackerDBContext _context;

        public AnalysesController(HormoneTrackerDBContext context)
        {
            _context = context;
        }

        // GET: api/Analyses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Analysis>>> GetAnalyses()
        {
            return await _context.Analyses.ToListAsync();
        }

        // GET: api/Analyses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Analysis>> GetAnalysis(int id)
        {
            var analysis = await _context.Analyses.FindAsync(id);

            if (analysis == null)
            {
                return NotFound();
            }

            return analysis;
        }

        [HttpGet("ByUserId/{id}")]
        public async Task<ActionResult<List<Analysis>>> GetAnalysesByUserId(int id)
        {
            return _context.Analyses.Where(a => a.PatientId == id).ToList();
        }

        // PUT: api/Analyses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalysis(int id, Analysis analysis)
        {
            if (id != analysis.AnalysisId)
            {
                return BadRequest();
            }

            _context.Entry(analysis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalysisExists(id))
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

        // POST: api/Analyses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Analysis>> PostAnalysis(Analysis analysis)
        {
            _context.Analyses.Add(analysis);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnalysisExists(analysis.AnalysisId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAnalysis", new { id = analysis.AnalysisId }, analysis);
        }

        // DELETE: api/Analyses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalysis(int id)
        {
            var analysis = await _context.Analyses.FindAsync(id);
            if (analysis == null)
            {
                return NotFound();
            }

            _context.Analyses.Remove(analysis);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnalysisExists(int id)
        {
            return _context.Analyses.Any(e => e.AnalysisId == id);
        }
    }
}
