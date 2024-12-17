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
    public class TipAbonamentsController : ControllerBase
    {
        private readonly ProiectContext _context;

        public TipAbonamentsController(ProiectContext context)
        {
            _context = context;
        }

        // GET: api/TipAbonaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipAbonament>>> GetTipAbonaments()
        {
          if (_context.TipAbonaments == null)
          {
              return NotFound();
          }
            return await _context.TipAbonaments.ToListAsync();
        }

        // GET: api/TipAbonaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipAbonament>> GetTipAbonament(int id)
        {
          if (_context.TipAbonaments == null)
          {
              return NotFound();
          }
            var tipAbonament = await _context.TipAbonaments.FindAsync(id);

            if (tipAbonament == null)
            {
                return NotFound();
            }

            return tipAbonament;
        }



        [HttpGet("{tip}/tipabonament")]
        public async Task<ActionResult<TipAbonament>> GetAbonamentNume(string tip)
        {
            if (_context.TipAbonaments == null)
            {
                return NotFound();
            }
            var tipAbonament = await _context.TipAbonaments.FirstOrDefaultAsync(c => c.Tip == tip);

            if (tipAbonament == null)
            {
                return NotFound();
            }

            return tipAbonament;
        }

        // PUT: api/TipAbonaments/5 //modifica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipAbonament(int id, TipAbonament tipAbonament)
        {
            if (id != tipAbonament.AbonamentId)
            {
                return BadRequest();
            }

            _context.Entry(tipAbonament).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipAbonamentExists(id))
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

        // POST: api/TipAbonaments //adauga
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipAbonament>> PostTipAbonament(TipAbonament tipAbonament)
        {
          if (_context.TipAbonaments == null)
          {
              return Problem("Entity set 'ProiectContext.TipAbonaments'  is null.");
          }

          var abonament2 = await _context.TipAbonaments.FirstOrDefaultAsync(c => c.Tip == tipAbonament.Tip);

            if (abonament2 is null)
            {
                _context.TipAbonaments.Add(tipAbonament);
                await _context.SaveChangesAsync();
            }
            else
            {
                return Problem("Tipul este deja");
            }
           

            return CreatedAtAction("GetTipAbonament", new { id = tipAbonament.AbonamentId }, tipAbonament);
        }

        // DELETE: api/TipAbonaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipAbonament(int id)
        {
            if (_context.TipAbonaments == null)
            {
                return NotFound();
            }
            var tipAbonament = await _context.TipAbonaments.FindAsync(id);
            if (tipAbonament == null)
            {
                return NotFound();
            }

            _context.TipAbonaments.Remove(tipAbonament);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipAbonamentExists(int id)
        {
            return (_context.TipAbonaments?.Any(e => e.AbonamentId == id)).GetValueOrDefault();
        }
    }
}
