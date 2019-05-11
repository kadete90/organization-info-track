using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InfoTrack.App.Models;
using InfoTrack.App.Utils;
using InfoTrack.Common.Contracts;
using InfoTrack.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InfoTrack.App.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public SearchController(ISearchService searchService, IMapper mapper, IMemoryCache cache)
        {
            _searchService = searchService;
            _mapper = mapper;
            _cache = cache;
        }

        // GET: /api/Search/History
        [HttpGet]
        [Route("History")]
        [ProducesResponseType(typeof(IEnumerable<SearchHistoryModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<SearchHistoryModel>> Index()
        {
            var cacheHistory = await _cache.GetOrCreateAsync(CacheKeys.SearchHistory, async cache =>
            {
                cache.SlidingExpiration = TimeSpan.FromSeconds(3);

                // TODO ADD logging in case of a error fetching db data | mapping to model
                var result = await _searchService.GetSearchHistoryAsync();
                return _mapper.Map<List<SearchHistoryModel>>(result.Result);
            });

            if (cacheHistory == null)
            {
                return null;
            }

            return cacheHistory;
        }

        // GET: /api/<controller>/Search?keyword=#keyword#&findUri=#uri#
        [HttpGet]
        [Route("FindUri")]
        [ProducesResponseType(typeof(SearchHistoryModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SearchHistoryModel>> SearchAsync(SearchModel mdl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _searchService.GetMatchAsync(mdl.keyword, mdl.findUri);

            if (result == null)
            {
                return BadRequest();
            }

            if (!result.Successful)
            {
                return BadRequest(result.Error);
            }

            _cache.Remove(CacheKeys.SearchHistory); // force update next history request

            return Ok(_mapper.Map<SearchHistoryModel>(result.Result));
        }
    }
}