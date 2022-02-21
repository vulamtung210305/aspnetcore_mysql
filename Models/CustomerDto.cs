
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class CustomerDto
    {
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        [MaxLength(15)]
        public string? Mobile { get; set; }
        [MaxLength(250)]
        public string? Email { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }
    }
}
