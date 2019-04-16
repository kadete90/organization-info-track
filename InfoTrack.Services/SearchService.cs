using HtmlAgilityPack;
using InfoTrack.Client;
using InfoTrack.DAL;
using InfoTrack.DAL.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InfoTrack.App.Models;

namespace InfoTrack.Services
{
    public interface ISearchService
    {
        // TODO create generic With Error Model
        Task<GenericResult<SearchHistory>> GetMatchAsync(string keyword, string findUri);
        Task<GenericResult<List<SearchHistory>>> GetSearchHistoryAsync();
    }

    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SearchService> _looger;

        private readonly IGoogleSearchClient _googleSearchClient;

        public SearchService(ApplicationDbContext context, ILogger<SearchService> looger, IGoogleSearchClient searchClient)
        {
            _context = context;
            _looger = looger;
            _googleSearchClient = searchClient;
        }

        string[] FilterLinks(HtmlDocument doc)
        {
            return doc.DocumentNode.Descendants("cite")
                .Select(c => c.InnerText)
                .ToArray();
        }

        public async Task<GenericResult<SearchHistory>> GetMatchAsync(string keyword, string findUri)
        {
            List<int> matchEntries;

            try
            {
                matchEntries = await _googleSearchClient.FetchPageAndFindUrl(keyword, findUri, FilterLinks);
            }
            catch (Exception ex)
            {
                string error = "Error fetching google search page";
                _looger.LogError(error, ex);

                return new GenericResult<SearchHistory>(error);
            }

            var result = new SearchHistory
            {
                Keyword = keyword,
                Url = findUri,
                SearchDate = DateTime.UtcNow
            };

            foreach (var entry in matchEntries)
            {
                result.SearchMatches.Add(new SearchMatch(entry));
            }

            try
            {
                await _context.SearchHistories.AddAsync(result);

                var changes = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string error = "Error saving google search results on the DB";
                _looger.LogError(error, ex);

                return new GenericResult<SearchHistory>(error);
            }

            return new GenericResult<SearchHistory>(result);
        }

        // TODO add pagination
        public async Task<GenericResult<List<SearchHistory>>> GetSearchHistoryAsync()
        {
            try
            {
                var searchHistory = await _context.SearchHistories
                    .Include(s => s.SearchMatches)
                    .ToListAsync();

                return new GenericResult<List<SearchHistory>>(searchHistory);
            }
            catch (Exception ex)
            {
                string error = "Error saving google search results on the DB";
                _looger.LogError(error, ex);

                return new GenericResult<List<SearchHistory>>(error);
            }
        }
    }
}