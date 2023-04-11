using Microsoft.EntityFrameworkCore;
using MovieAPi.Dtos;
using MovieAPi.Models;

namespace MovieAPi.Services
{
    public class MovieServices : IMoviesServices
    {
        private readonly MovieContext movieContext ;

        public MovieServices(MovieContext movieContext)
        {
            this.movieContext = movieContext;
        }

        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            await movieContext.AddAsync(movie);
            movieContext.SaveChanges();
            return movie;

        }

        public Movie DeleteMovie(Movie movie)
        {
            movieContext.Remove(movie);
            movieContext.SaveChanges();
            return movie;

        }

        public async Task<IEnumerable<MovieDetailsDto>> GetAllasync(int genreId = 0)
        {
            return
           await  movieContext.Movies
                 .Where(m => m.GenreId == genreId || genreId==0)
                .OrderByDescending(R => R.Rate)
                 .Include(g => g.Genre)
                 .Select(s => new MovieDetailsDto
                 {
                     Rate = s.Rate,
                     GenreId = s.GenreId,
                     Storeline = s.Storeline,
                     Title = s.Title,
                     GenreName = s.Genre.Name,
                     Year = s.Year,
                     Id = s.Id
                 }).ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var Movie = await movieContext.Movies.Include(m => m.Genre).SingleAsync(s => s.Id == id);
            return Movie;

        }


        public Movie UpdateMovie(Movie movie)
        {
            movieContext.Update(movie);
            movieContext.SaveChanges();
            return movie;

        }
    }
}
