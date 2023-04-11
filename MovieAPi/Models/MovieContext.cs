using Microsoft.EntityFrameworkCore;

namespace MovieAPi.Models
{
    public class MovieContext:DbContext
    {
        public MovieContext( DbContextOptions<MovieContext> options):base(options)
        {}

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get;set; }

    }
}
