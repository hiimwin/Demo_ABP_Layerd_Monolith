using abpSourceCode.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace abpSourceCode.OrderItems
{
    public class OrderItemDto : AuditedEntityDto<long>
    {
        public int Quanlity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quanlity;
        public string? Notes { get; set; }
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public BookDto Book { get; set; } = default!;
    }
}
