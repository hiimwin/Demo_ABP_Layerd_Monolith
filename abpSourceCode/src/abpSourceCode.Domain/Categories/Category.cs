using abpSourceCode.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace abpSourceCode.Categories
{
    public class Category : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //(1 -> nhiều)
        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
