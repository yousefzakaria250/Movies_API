using Microsoft.AspNetCore.Mvc;
using MovieAPi.Dtos;
using MovieAPi.Models;

namespace MovieAPi.Services
{
    public interface IMoviesServices
    {
        public  Task<IEnumerable<MovieDetailsDto>> GetAllasync(int genreId = 0);
        public Task<Movie> GetMovieById(int id);
        public Task<Movie> AddMovieAsync(Movie movie);
        public Movie UpdateMovie(Movie movie);
        public Movie DeleteMovie(Movie movie);
       
    }
}
