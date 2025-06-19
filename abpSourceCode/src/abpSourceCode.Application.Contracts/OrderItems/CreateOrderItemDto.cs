using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abpSourceCode.OrderItems
{
    public class CreateOrderItemDto
    {
        public int Quanlity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Notes { get; set; }
        public Guid BookId { get; set; }
    }
}
