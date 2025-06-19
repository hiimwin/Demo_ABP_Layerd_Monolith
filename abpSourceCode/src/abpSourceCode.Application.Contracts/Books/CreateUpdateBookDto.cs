using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abpSourceCode.Books
{
    public class CreateUpdateBookDto
    {
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
