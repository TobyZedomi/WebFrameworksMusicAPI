﻿namespace WebFrameworksMusicAPI.DTOs
{
    public class AlbumPutDto
    {

        public int Id { get; set; }

        public string AlbumName { get; set; }

        public int NumberOfSongs { get; set; }

        public int NumberOfFeatures { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int ArtistId { get; set; }
    }
}
