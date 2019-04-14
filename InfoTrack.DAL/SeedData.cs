using InfoTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.DAL
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            await SeedSearchHistoryAsync(context);
        }

        private static async Task SeedSearchHistoryAsync(ApplicationDbContext context)
        {
            List<SearchHistory> searches = new List<SearchHistory>
            {   
                new SearchHistory { Id = Guid.NewGuid(), Url = "www.infotrack.co.uk", Keyword = "land registry searches", Matches = 4, SearchDate = DateTime.UtcNow }
            };

            foreach (var search in searches)
            {
                if (!await context.SearchHistories.AnyAsync(s => s.Url == search.Url && s.Keyword == search.Keyword && s.SearchDate == search.SearchDate))
                {
                    await context.SearchHistories.AddAsync(search);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}