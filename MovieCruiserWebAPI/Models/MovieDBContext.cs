using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCruiserWebAPI.Models
{
    public class MovieDBContext: DbContext
    {   
        public DbSet<MovieList> MovieList { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Login> Login { get; set; }
        public MovieDBContext(DbContextOptions options): base (options)
        {

        }
    }
}
