using System.ComponentModel.DataAnnotations;

namespace MovieAPi.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

      
        public string Storeline { get; set; }

        public IFormFile Poster { get; set; }

        public int GenreId { get; set; }

        public string GenreName { get; set; }   


    }
}
