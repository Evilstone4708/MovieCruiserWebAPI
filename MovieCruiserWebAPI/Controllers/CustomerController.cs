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
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        IMovieListOperations movie;
        public CustomerController(IMovieListOperations movie)
        {
            this.movie = movie;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(movie.GetMovieCustomerAsync());
        }
    }
}
