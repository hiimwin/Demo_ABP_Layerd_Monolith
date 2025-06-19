using abpSourceCode.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace abpSourceCode.Payments
{
    public class Payment : AuditedAggregateRoot<Guid>
    {
        public decimal? Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateOnly DateTime { get; set; }
        public PaymentStatus status { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
