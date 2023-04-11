using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPi.Dtos;
using MovieAPi.Models;
using MovieAPi.Services;
using System.Security.Cryptography.X509Certificates;

namespace MovieAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesServices moviesServices;
        private readonly IGenresServices genresServices;

       

        public MoviesController(IMoviesServices moviesServices , IGenresServices genresServices)
        {
            this.moviesServices = moviesServices;
            this.genresServices = genresServices;
        }

        List<string> AllowedExtension = new List<string> { ".jpg", ".png" };
        long AllowedSize = 1048576;

        [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovieAsync( [FromForm]CreateMovieDto dto)
        {
            // Check Extension
            if( !AllowedExtension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower())) 
                return BadRequest("File Exstnsion should be JPG , PNG Only");
            
            // Check Size for Image

            if( dto.Poster.Length > AllowedSize)
                return BadRequest("File Size more than Allowed" );

            // Check Genre IS Exsist in Database .
            var IsValidGenre = await genresServices.IsValidGenre(dto.GenreId);
            if (!IsValidGenre)
                return BadRequest("Not Found Genre with this Name");

            var datamemory = new MemoryStream();
            dto.Poster.CopyTo(datamemory);
            Movie movie = new Movie
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                Storeline = dto.Storeline,
                Poster = datamemory.ToArray() 
            };

            await moviesServices.AddMovieAsync(movie);
            return Ok(movie);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllasync()
        {

            var Movies = await moviesServices.GetAllasync();
            if (Movies == null)
                return Ok("don't Film Exsisting in Movies");
            return Ok(Movies);

            //var Movies = await moviesServices.Movies
            //    .OrderByDescending(m=>m.Rate)            
            //    .Include(m => m.Genre)

            //    .Select(m => new MovieDetailsDto
            //        {
            //            GenreId = m.GenreId,
            //            Title = m.Title,
            //            Year = m.Year,
            //            Rate = m.Rate,
            //            Storeline = m.Storeline,
            //            Id = m.Id,
            //            //Poster = m.Poster,
            //                     GenreName = m.Genre.Name
            //                 })
            //                  .ToListAsync();


        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await moviesServices.GetMovieById(id);
            if (movie == null)
                return NotFound($"Movie Not Found by {id}");
            return Ok(movie);
            //var Movie = new MovieDetailsDto
            //{
            //    GenreId = movie.GenreId,
            //    Title = movie.Title,
            //    Year = movie.Year,
            //    Rate = movie.Rate,
            //    Storeline = movie.Storeline,
            //    Id = movie.Id,
            //    GenreName = movie.Genre.Name
            //};

        } 
        [HttpGet("FindByGenreId")]
        public async Task<IActionResult> GetByGenreId(int id)
        {
            var movies = await moviesServices.GetAllasync(id);
                
            if (movies == null)
                return NotFound($"don't exsist by {id}");
            else return Ok(movies);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteMovieAsync( int id)
        {
            var movie = 
                await moviesServices.GetMovieById(id);     
            if (movie == null)
                return NotFound();

            moviesServices.DeleteMovie(movie);
            return Ok(movie);
        }


        [HttpPut("id")]
        public async Task<IActionResult> UpdateMovieAsync(int id , [FromForm] CreateMovieDto dto)
        {
            var movie = await moviesServices.GetMovieById(id);
            if (movie == null)
                return NotFound($"Not Movies with {id}");
            var IsValidGenre = await genresServices.IsValidGenre(dto.GenreId);
            if (!IsValidGenre)
                return BadRequest("Not Found Genre with this Name");
            if (dto.Poster != null)
            {
                // Check Extension
                if (!AllowedExtension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("File Exstnsion should be JPG , PNG Only");

                // Check Size for Image

                if (dto.Poster.Length > AllowedSize)
                    return BadRequest("File Size more than Allowed");
                var datamemory = new MemoryStream();
                dto.Poster.CopyTo(datamemory)
                        ;
                movie.Poster = datamemory.ToArray();
            }
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            //movie.Id = dto.Id;
            movie.Storeline = dto.Storeline;
            //movie.Poster = datamemory.ToArray();
            movie.Rate = dto.Rate;
            movie.GenreId = dto.GenreId;
            moviesServices.UpdateMovie(movie);
            return Ok(movie);
        }
    }
}
