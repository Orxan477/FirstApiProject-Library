using System.ComponentModel.DataAnnotations;

namespace FirstRestApi_Library_.DTOs.BookDto
{
    public class UpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
