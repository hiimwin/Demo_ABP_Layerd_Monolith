using abpSourceCode.OrderItems;
using abpSourceCode.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace abpSourceCode.Orders
{
    public class OrderDto : AuditedEntityDto<Guid>
    {
        public DateOnly? OrderDate { get; set; }
        public decimal TotalAmout { get; set; }
        public string Address { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
        public PaymentDto Payment { get; set; }
    }
}
