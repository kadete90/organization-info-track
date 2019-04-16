using System;
namespace InfoTrack.Common.Contracts
{
    public class SearchHistoryModel
    {
        public string Url { get; set; }
        public string Keyword { get; set; }

        public string SearchDate { get; set; }

        public string MatchEntries { get; set; }
    }
}
