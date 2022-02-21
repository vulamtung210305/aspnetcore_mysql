
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    [Table("orders")]
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("order_date")]
        public DateTime? OrderDate { get; set; }
        [Column("due_date")]
        public DateTime? DueDate { get; set; }
        [Column("gross_amount", TypeName = "decimal(15, 2)")]
        public decimal GrossAmount { get; set; }
        [Column("tax_amount", TypeName = "decimal(15, 2)")]
        public decimal TaxAmount { get; set; }
        [Column("due_amount", TypeName = "decimal(15, 2)")]
        public decimal DueAmount { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("created_by")]
        [MaxLength(50)]
        public string? CreatedBy { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [Column("updated_by")]
        [MaxLength(50)]
        public string? UpdatedBy { get; set; }
        [Column("customer_id")]
        [ForeignKey("customers")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
