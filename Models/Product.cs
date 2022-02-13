
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    [Table("products")]
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [MaxLength(250)]
        public string? Name { get; set; }
        [Column("price", TypeName = "decimal(15, 2)")]
        public decimal Price { get; set; }
        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }
    }
}
