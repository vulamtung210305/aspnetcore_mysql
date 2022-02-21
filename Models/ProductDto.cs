
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
    }
}
