using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICore.Data;
using WebAPICore.Models;

namespace WebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbonatiisController : ControllerBase
    {
        private readonly ProiectContext _context;

        public AbonatiisController(ProiectContext context)
        {
            _context = context;
        }

        // GET: api/Abonatiis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abonatii>>> GetAbonatiis()
        {
          if (_context.Abonatiis == null)
          {
              return NotFound();
          }
            return await _context.Abonatiis.ToListAsync();
        }

        // GET: api/Abonatiis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Abonatii>> GetAbonatii(int id)
        {
          if (_context.Abonatiis == null)
          {
              return NotFound();
          }
            var abonatii = await _context.Abonatiis.FindAsync(id);

            if (abonatii == null)
            {
                return NotFound();
            }

            return abonatii;
        }

        // PUT: api/Abonatiis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbonatii(int id, Abonatii abonatii)
        {
            if (id != abonatii.AbonatiiId)
            {
                return BadRequest();
            }

            _context.Entry(abonatii).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonatiiExists(id))
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

        // POST: api/Abonatiis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Abonatii>> PostAbonatii(Abonatii abonatii)
        {
          if (_context.Abonatiis == null)
          {
              return Problem("Entity set 'ProiectContext.Abonatiis'  is null.");
          }

            var abonatClient = await _context.Clients.FindAsync(abonatii.ClientId);
            var abonatTipAbonament= await _context.TipAbonaments.FindAsync(abonatii.AbonamentId);

            if (abonatClient is null || abonatTipAbonament is null)
            {
                return Problem("Clientul sau tipul abonamentului nu exista.");
            }
            else
            {
                _context.Abonatiis.Add(abonatii);
                await _context.SaveChangesAsync();
            }
          

            return CreatedAtAction("GetAbonatii", new { id = abonatii.AbonatiiId }, abonatii);
        }

        // DELETE: api/Abonatiis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbonatii(int id)
        {
            if (_context.Abonatiis == null)
            {
                return NotFound();
            }
            var abonatii = await _context.Abonatiis.FindAsync(id);
            if (abonatii == null)
            {
                return NotFound();
            }

            _context.Abonatiis.Remove(abonatii);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbonatiiExists(int id)
        {
            return (_context.Abonatiis?.Any(e => e.AbonatiiId == id)).GetValueOrDefault();
        }
    }
}
