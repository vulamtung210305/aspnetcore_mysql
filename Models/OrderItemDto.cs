
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class OrderItemDto
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
