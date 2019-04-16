using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoTrack.DAL.Entities
{
    public class SearchMatch
    {
        public Guid SearchHistoryId { get; set; }

        public int Entry { get; set; }

        [ForeignKey("SearchHistoryId")]
        public SearchHistory SearchHistory { get; set; }

        public SearchMatch(int entry)
        {
            Entry = entry;
        }
    }
}