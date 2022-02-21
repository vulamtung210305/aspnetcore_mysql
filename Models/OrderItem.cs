
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    [Table("order_items")]
    public class OrderItem
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price", TypeName = "decimal(15, 2)")]
        public decimal Price { get; set; }

        [Column("gross_amount", TypeName = "decimal(15, 2)")]
        public decimal GrossAmount { get; set; }
        [Column("tax_amount", TypeName = "decimal(15, 2)")]
        public decimal TaxAmount { get; set; }
        [Column("due_amount", TypeName = "decimal(15, 2)")]
        public decimal DueAmount { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [Column("order_id")]
        [ForeignKey("orders")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Column("product_id")]
        [ForeignKey("products")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
