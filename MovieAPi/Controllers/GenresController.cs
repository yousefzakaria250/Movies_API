using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPi.Dtos;
using MovieAPi.Models;
using MovieAPi.Services;

namespace MovieAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
       private readonly IGenresServices _genresServices;

        public GenresController(IGenresServices genresServices)
        {
            _genresServices = genresServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresServices.GetAllAsync();
            return Ok(genres);
        }

        [HttpPost]

        public async Task<IActionResult> AddGenreAsync([FromBody] CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };

            await _genresServices.AddGenreAsync(genre);
            return Ok(genre);
        
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenreAsync( int id , CreateGenreDto dto)
        {
            var genre = await _genresServices.GetGenreById(id);
            if (genre == null)
            {
                return NotFound($"Not Found Id with {id}");
            }
            genre.Name = dto.Name;
             _genresServices.UpdateGenreAsync(genre);
            return Ok(genre);

        }


        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre = await _genresServices.GetGenreById(id);
            if (genre == null)
            {
                return  NotFound($"Not Found Genre with {id}") ;
            }
            _genresServices.DeleteGenreAsync(genre);
            return Ok(genre);

        }
    }
}
