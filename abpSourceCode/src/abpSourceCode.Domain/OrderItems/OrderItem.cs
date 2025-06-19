using abpSourceCode.Books;
using abpSourceCode.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace abpSourceCode.OrderItems
{
    public class OrderItem : AuditedAggregateRoot<long>
    {
        public int Quanlity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quanlity;
        public string? Notes { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = default!;
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; } = default!;
    }
}
