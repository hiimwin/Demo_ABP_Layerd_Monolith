using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abpSourceCode.Payments
{
    public enum PaymentStatus
    {
        Pending,       // Chờ thanh toán
        Paid,          // Đã thanh toán
        Failed,        // Thanh toán thất bại
        Cancelled,     // Đã hủy
        Refunded        // Đã hoàn tiền
    }
}
