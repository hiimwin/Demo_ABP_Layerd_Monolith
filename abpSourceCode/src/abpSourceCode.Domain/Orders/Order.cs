using abpSourceCode.OrderItems;
using abpSourceCode.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace abpSourceCode.Orders
{
    public class Order : AuditedAggregateRoot<Guid>
    {
        public DateOnly? OrderDate { get; set; }
        public decimal TotalAmout { get; set; }
        public string Address { get; set; }
        public OrderStatus Status { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public Payment? Payment { get; set; }
    }
}
