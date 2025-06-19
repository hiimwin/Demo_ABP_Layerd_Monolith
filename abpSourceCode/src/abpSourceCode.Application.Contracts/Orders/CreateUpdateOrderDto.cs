using abpSourceCode.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abpSourceCode.Orders
{
    public class CreateUpdateOrderDto
    {
        public DateOnly? OrderDate { get; set; }
        public decimal TotalAmout { get; set; }
        public string Address { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<CreateOrderItemDto> OrderItems { get; set; } = new HashSet<CreateOrderItemDto>();
    }
}
