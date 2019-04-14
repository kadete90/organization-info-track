using System.Threading.Tasks;
using InfoTrack.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoTrack.App.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            this._searchService = searchService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
