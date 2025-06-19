using abpSourceCode.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace abpSourceCode.Authors
{
    public class Author : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public string AvartalUrl { get; set; }
        //(1 -> nhiều)
        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();

    }
}
