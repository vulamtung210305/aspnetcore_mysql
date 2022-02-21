
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Models
{

    public class OrderDto
    {

        public int? Id { get; set; }
        [Required]
        public DateTime? OrderDate { get; set; }
        [Required]
        public DateTime? DueDate { get; set; }
        [Required]
        public CustomerDto? Customer { get; set; }
        [Required]
        public List<OrderItemDto> OrderItems { get; set; }
        public static OrderDto Create()
        {
            return new OrderDto()
            {
                Customer = new CustomerDto(),
                OrderItems = new List<OrderItemDto>(){
                    new OrderItemDto()
                }
            };
        }
        public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();
    }
}
