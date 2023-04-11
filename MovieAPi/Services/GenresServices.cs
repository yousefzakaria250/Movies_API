using Microsoft.EntityFrameworkCore;
using MovieAPi.Models;

namespace MovieAPi.Services
{
    public class GenresServices : IGenresServices
    {
        private readonly MovieContext Context;

        public GenresServices(MovieContext Context)
        {
            this.Context = Context;
        }


        public async Task<Genre> AddGenreAsync(Genre genre)
        {
            await Context.AddAsync(genre);
            Context.SaveChanges();
            return genre;
        }

        public Genre DeleteGenreAsync(Genre genre)
        {
            Context.Remove(genre);
            Context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return
                await Context.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Genre> GetGenreById(int id)
        {
           return
                await  Context.Genres.SingleAsync(g=>g.Id == id);
            
        }

        public Genre UpdateGenreAsync(Genre genre)
        {
            Context.Update(genre);
            Context.SaveChanges();
            return genre;

        }
        public async Task<bool> IsValidGenre(int GenreId)
        {
            return
            await Context.Genres.AnyAsync(g => g.Id == GenreId);

        }
    }
}
