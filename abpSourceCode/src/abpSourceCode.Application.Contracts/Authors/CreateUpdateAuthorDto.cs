using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abpSourceCode.Authors
{
    public class CreateUpdateAuthorDto
    {
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public string AvartalUrl { get; set; }
    }
}
