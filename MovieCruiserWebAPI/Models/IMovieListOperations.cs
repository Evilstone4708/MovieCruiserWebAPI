using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieCruiserWebAPI.Models
{
    public interface IMovieListOperations
    {
        Task<List<MovieList>> GetMovieAdminAsync();
        Task<List<MovieList>> GetMovieAnonymousAsync();
        Task<List<MovieList>> GetMovieCustomerAsync();
        Task<MovieList> GetMovieById(int id);
        Task<MovieList> UpdateMovieList(MovieList mvs);
        Task<List<MovieList>> SearchMovieAdminAsync(string name);
        Task<List<MovieList>> SearchMovieNonAdminAsync(string name);


    }
}