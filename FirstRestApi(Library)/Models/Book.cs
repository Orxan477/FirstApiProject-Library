using System;
using System.ComponentModel.DataAnnotations;

namespace FirstRestApi_Library_.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
