using GameStore.DataBase;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GameStore.Controllers
{
    [Route("/[controller]")]
    public class SearchController : Controller
    {
        private readonly SearchService _searchService;
        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet, Route("Filter")]
        public async Task<IActionResult> Search([FromBody] SearchModel search)
        {
            try
            {
                if(string.IsNullOrEmpty(search.SearchValue))
                {
                    return new OkObjectResult(await _searchService.Search(""));
                }
                else
                    return new OkObjectResult(await _searchService.Search(search.SearchValue.ToString()));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("Filter/{id})")]
        public async Task<IActionResult> FilterByGenre(int id)
        {
            return new OkObjectResult(await _searchService.FilterByGenre(id));
        }
    }
}
