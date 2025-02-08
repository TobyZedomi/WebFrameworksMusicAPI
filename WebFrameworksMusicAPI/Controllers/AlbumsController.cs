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
    public class AlbumsController : ControllerBase
    {
        private readonly MusicContext _context;

        public AlbumsController(MusicContext context)
        {
            _context = context;
        }

        // GET: api/Albums
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbum([FromQuery] QueryObjectAlbum query)
        {

            if(_context.Album == null)
            {
                return NotFound();
            }

            var albums = _context.Album.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.AlbumName))
            {
                albums = albums.Where(a => a.AlbumName.Contains(query.AlbumName));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {

                if(query.SortBy.Equals("AlbumName", StringComparison.OrdinalIgnoreCase))
                {
                    albums  = query.IsDescending ? albums.OrderByDescending(a => a.AlbumName) : albums.OrderBy(a => a.AlbumName);
                }


                if (query.SortBy.Equals("AlbumId", StringComparison.OrdinalIgnoreCase))
                {
                    albums = query.IsDescending ? albums.OrderByDescending(a => a.Id) : albums.OrderBy(a => a.Id);
                }

                if (query.SortBy.Equals("ArtistId", StringComparison.OrdinalIgnoreCase))
                {
                    albums = query.IsDescending ? albums.OrderByDescending(a => a.ArtistId) : albums.OrderBy(a => a.ArtistId);
                }

                if (query.SortBy.Equals("NumberOfSongs", StringComparison.OrdinalIgnoreCase))
                {
                    albums = query.IsDescending ? albums.OrderByDescending(a => a.NumberOfSongs) : albums.OrderBy(a => a.NumberOfSongs);

                }

                if (query.SortBy.Equals("ReleaseDate", StringComparison.OrdinalIgnoreCase))
                {
                    albums = query.IsDescending ? albums.OrderByDescending(a => a.ReleaseDate) : albums.OrderBy(a => a.ReleaseDate);

                }


            }

            var albumList = await albums.Select(t =>
            new AlbumGetDto()
            {
                Id = t.Id,
                AlbumName = t.AlbumName,
                NumberOfSongs = t.NumberOfSongs,
                ReleaseDate = t.ReleaseDate,
                ArtistId = t.ArtistId
            }

            ).ToListAsync();
            return Ok(albumList);
        }

        // GET: api/Albums/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var album = await _context.Album.FindAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        // PUT: api/Albums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutAlbum(int id, AlbumPutDto albumDto)
        {

            Album album = new()
            {
                Id = albumDto.Id,
                AlbumName = albumDto.AlbumName,
                NumberOfSongs = albumDto.NumberOfSongs,
                NumberOfFeatures = albumDto.NumberOfFeatures,
                ReleaseDate = albumDto.ReleaseDate,
                ArtistId = albumDto.ArtistId,
            };


            if (id != album.Id)
            {
                return BadRequest();
            }

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
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

        // POST: api/Albums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AlbumPostDto>> PostAlbum(AlbumPostDto albumDto)
        {

            Album album = new()
            {
                Id = albumDto.Id,
                AlbumName = albumDto.AlbumName,
                NumberOfSongs = albumDto.NumberOfSongs,
                NumberOfFeatures = albumDto.NumberOfFeatures,
                ReleaseDate = albumDto.ReleaseDate,
                ArtistId = albumDto.ArtistId,
            };

            _context.Album.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlbum), new { id = album.Id }, album);
        }

        // DELETE: api/Albums/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Album.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.Id == id);
        }
    }
}
