using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCruiserWebAPI.Models
{
    public class MovieListOperations : IMovieListOperations
    {
        private readonly MovieDBContext movie;
        public MovieListOperations(MovieDBContext m)
        {
            this.movie = m;
        }
        public async Task<List<MovieList>> GetMovieAdminAsync()
        {
            var data = from m in movie.MovieList
                       select m;
            var movieItems = await data.ToListAsync();
            return movieItems;
        }
        public async Task<List<MovieList>> GetMovieAnonymousAsync()
        {
            return await GetMovieCustomerAsync();
        }
        public async Task<List<MovieList>> GetMovieCustomerAsync()
        {
            var data = from m in movie.MovieList
                       where m.IsAvailable && m.LaunchDate <= DateTime.Now
                       select m;
            var movieItems = await data.ToListAsync();
            return movieItems;

        }
        public async Task<List<MovieList>> SearchMovieAdminAsync(string name)
        {
            var items = from f in movie.MovieList
                        select f;
            var movieList = new List<MovieList>();
            name = name.ToUpper().Trim();
            await items.ForEachAsync(f =>
            {
                var temp = f.Name.ToUpper();
                if (temp.StartsWith(name))
                {
                    movieList.Add(f);
                }
            });

            return movieList;
        }
        public async Task<MovieList> GetMovieById(int id)
        {
            return await movie.MovieList.FindAsync(id);
        }
        public async Task<MovieList> UpdateMovieList(MovieList mvs)
        {
            var item = movie.MovieList.Find(mvs.Name);
            if (item != null)
            {
                item.Name = mvs.Name;
                item.Price = mvs.Price;
                item.IsAvailable = mvs.IsAvailable;
                item.LaunchDate = mvs.LaunchDate;
                item.ImageURL = mvs.ImageURL;
            }
            await movie.SaveChangesAsync();

            return item;
        }
        public async Task<List<MovieList>> SearchMovieNonAdminAsync(string name)
        {
            var items = from m in movie.MovieList
                        where m.IsAvailable && m.LaunchDate <= DateTime.Now
                        select m;
            var movieList = new List<MovieList>();
            name = name.ToUpper().Trim();
            await items.ForEachAsync(m =>
            {
                var tempName = m.Name.ToUpper();
                if (tempName.StartsWith(name))
                {
                    movieList.Add(m);
                }
            });
            return movieList;
        }
    }
}
