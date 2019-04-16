using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoTrack.DAL.Entities
{
    public class SearchHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Url { get; set; }
        [Required]
        public string Keyword { get; set; }

        public DateTime SearchDate { get; set; } 

        public ICollection<SearchMatch> SearchMatches { get; set; } = new List<SearchMatch>();
    }
}