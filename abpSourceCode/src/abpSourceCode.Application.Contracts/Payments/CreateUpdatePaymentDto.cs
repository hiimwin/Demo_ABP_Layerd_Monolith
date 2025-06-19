using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abpSourceCode.Payments
{
    public class CreateUpdatePaymentDto
    {
        public decimal? Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateOnly DateTime { get; set; }
        public PaymentStatus status { get; set; }
        public Guid OrderId { get; set; }
    }
}
