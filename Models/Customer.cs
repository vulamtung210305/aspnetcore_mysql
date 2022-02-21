
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    [Table("customers")]
    public class Customer
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [MaxLength(250)]
        public string Name { get; set; }
        [Column("mobile")]
        [MaxLength(15)]
        public string? Mobile { get; set; }
        [Column("email")]
        [MaxLength(250)]
        public string? Email { get; set; }
        [Column("address")]
        [MaxLength(500)]
        public string? Address { get; set; }
        [Column("registered_date")]
        public DateTime? RegisteredDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
