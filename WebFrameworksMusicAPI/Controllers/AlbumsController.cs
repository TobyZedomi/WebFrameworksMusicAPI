﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class AlbumsController : ControllerBase
    {
        private readonly MusicContext _context;

        public AlbumsController(MusicContext context)
        {
            _context = context;
        }

        // GET: api/Albums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbum()
        {
            return await _context.Album.ToListAsync();
        }

        // GET: api/Albums/5
        [HttpGet("{id}")]
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
        public async Task<IActionResult> PutAlbum(int id, Album album)
        {
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
