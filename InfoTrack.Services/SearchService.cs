using InfoTrack.DAL;
using Microsoft.Extensions.Logging;

namespace InfoTrack.Services
{
    public interface ISearchService
    {

    }

    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SearchService> _looger;

        public SearchService(ApplicationDbContext context, ILogger<SearchService> looger)
        {
            this._context = context;
            this._looger = looger;
        }

    }
}
