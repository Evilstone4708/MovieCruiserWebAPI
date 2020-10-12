using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCruiserWebAPI.Models;

namespace MovieCruiserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        IMovieListOperations movieListoperation;
        public AnonymousController(IMovieListOperations operation)
        {
            this.movieListoperation = operation;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {   

            var items = await movieListoperation.GetMovieAnonymousAsync();

            if (items.Count == 0)
                return BadRequest("No movies");
            return Ok(items);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            var itemList = await movieListoperation.SearchMovieNonAdminAsync(name);

            if (itemList==null)
                return NotFound("Object cannot be found");

            return Ok(itemList);
        }
    }
}
