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
using WebFrameworksMusicAPI.Helpers;
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
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtist([FromQuery] QueryObjectArtist query)
        {
            if (_context.Artist == null)
            {
                return NotFound();
            }

            var artists = _context.Artist.AsQueryable();

            // getting artist by artist name entered

            if (!string.IsNullOrWhiteSpace(query.ArtistName))
            {
                artists = artists.Where(a => a.ArtistName.Contains(query.ArtistName));
            }

            // getting artist by genre 


            if (!string.IsNullOrWhiteSpace(query.Genre))
            {
                artists = artists.Where(a => a.Genre.Contains(query.Genre));
            }

            // Sorting artist by artistname, genre or artistId. Can be ascending or descending order

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("ArtistName", StringComparison.OrdinalIgnoreCase))
                {
                    artists = query.IsDescending ? artists.OrderByDescending(a => a.ArtistName) : artists.OrderBy(a => a.ArtistName);
                }

                if (query.SortBy.Equals("Genre", StringComparison.OrdinalIgnoreCase))
                {
                    artists = query.IsDescending ? artists.OrderByDescending(a => a.Genre) : artists.OrderBy(a => a.Genre);
                }

                if (query.SortBy.Equals("ArtistId", StringComparison.OrdinalIgnoreCase))
                {
                    artists = query.IsDescending ? artists.OrderByDescending(a => a.Id) : artists.OrderBy(a => a.Id);
                }



            }

            // using ArtistGetDTO to filter information given to the user

            var artistList = await artists.Select(t =>
            new ArtistGetDto()
            {
                Id = t.Id,
                ArtistName = t.ArtistName,
                Genre = t.Genre
            }

            ).ToListAsync();
            return Ok(artistList);
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
        public async Task<IActionResult> PutArtist(int id, ArtistPutDto artistDto)
        {

            //using ArtistPutDto class to filter data

            Artist artist = new()
            {
                Id = artistDto.Id,
                ArtistName = artistDto.ArtistName,
                Genre = artistDto.Genre,
                DateOfBirth = artistDto.DateOfBirth,
            };


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
            // using ArtistPostDto to filter data

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
