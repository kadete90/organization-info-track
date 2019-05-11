using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.Client
{
    public interface IGoogleSearchClient
    {
        Task<List<int>> FetchPageAndFindUrl(string keyword, string matchUrl, Func<HtmlDocument, string[]> filter);
    }

    public class GoogleSearchClient : IGoogleSearchClient
    {
        private const string baseGoogleQuery = "https://www.google.co.uk/search?num=100&q=#keyword";

        public async Task<List<int>> FetchPageAndFindUrl(string keyword, string matchUrl, Func<HtmlDocument, string[]> filter)
        {
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(baseGoogleQuery.Replace("#keyword", keyword));

            var allCites = filter(doc);

            var entryMatches = new List<int>();

            for (int i = 0; i < allCites.Length; i++){
                if (allCites[i].Contains(matchUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    entryMatches.Add(i+1);
                }
            }

            return entryMatches;
        }
    }
}