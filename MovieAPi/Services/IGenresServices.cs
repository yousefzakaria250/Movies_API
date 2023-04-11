using Microsoft.AspNetCore.Mvc;
using MovieAPi.Dtos;
using MovieAPi.Models;

namespace MovieAPi.Services
{
    public interface IGenresServices
    {

        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetGenreById(int id);
        Task<Genre> AddGenreAsync(Genre genre);
        Genre UpdateGenreAsync(Genre genre);
        Genre DeleteGenreAsync(Genre genre);
        public Task<bool> IsValidGenre(int GenreId);

    }
}
