using System;
using System.ComponentModel.DataAnnotations;

namespace InfoTrack.DAL.Entities
{
    public class SearchHistory
    {
        public Guid Id { get; set; }

        [Required]
        public string Url { get; set; }
        [Required]
        public string Keyword { get; set; }

        public int Matches { get; set; }

        public DateTime SearchDate { get; set; } 
    }
}
