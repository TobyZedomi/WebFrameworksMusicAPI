using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrameworksMusicAPI.Data;
using WebFrameworksMusicAPI.DTOs;
using WebFrameworksMusicAPI.Model;

namespace WebFrameworksMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly MusicContext _context;

        public ArtistsController(MusicContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtist()
        {
            if (_context.Artist == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist.Select(t =>
            new Artist()
            {
                Id = t.Id,
                ArtistName = t.ArtistName,
                Genre = t.Genre
            }

            ).ToListAsync();

            return await _context.Artist.ToListAsync();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artist = await _context.Artist.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            if (id != artist.Id)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ArtistPostDto>> PostArtist(ArtistPostDto artistDto)
        {
            Artist artist = new()
            {
                Id = artistDto.Id,
                ArtistName = artistDto.ArtistName,
                Genre = artistDto.Genre,
                DateOfBirth = artistDto.DateOfBirth,
            };

            _context.Artist.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return _context.Artist.Any(e => e.Id == id);
        }
    }
}
