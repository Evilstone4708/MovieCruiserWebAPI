using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCruiserWebAPI.Models;

namespace MovieCruiserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]

    public class AdminController : ControllerBase
    {
        private readonly IMovieListOperations movie;
        public AdminController(IMovieListOperations operation)
        {
            this.movie = operation;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await movie.GetMovieAdminAsync();

            if (items.Count == 0)
                return BadRequest("No Movies");
            return Ok(items);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var item = await movie.GetMovieById(id);

            if (item == null)
                return NotFound("No movie with given id");

            return Ok(item);
        }
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            var itemList = await movie.SearchMovieAdminAsync (name);

            if (itemList.Count == 0)
                return NotFound("Movie cannot be found");

            return Ok(itemList);
        }


    }
}
