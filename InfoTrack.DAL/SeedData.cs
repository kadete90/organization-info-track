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

        private readonly static List<string> initGuids = new List<string>
        {
            "893e239f-bef3-4825-a25c-b7f9d0a0d47b",
            "d4748bda-b5c3-4de7-9313-3307b60b59d2"
        };

        private static async Task SeedSearchHistoryAsync(ApplicationDbContext context)
        {
            foreach(var initGuid in initGuids)
            {
                if (!Guid.TryParse(initGuid, out Guid guid))
                {
                    guid = Guid.NewGuid();
                }

                if (!await context.SearchHistories.AnyAsync(s => s.Id == guid))
                {
                    var search = new SearchHistory
                    {
                        Id = guid,
                        Url = "www.seed_data.co.uk",
                        Keyword = "seed keyword",
                        SearchDate = DateTime.UtcNow
                    };

                    var random = new Random();
                    var entries = new HashSet<int>();

                    for (int i = 0; i < 5; i++)
                    {
                        entries.Add(random.Next(1, 101));
                    }

                    foreach (var entry in entries)
                    {
                        search.SearchMatches.Add(new SearchMatch(entry));
                    }

                    await context.SearchHistories.AddAsync(search);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}