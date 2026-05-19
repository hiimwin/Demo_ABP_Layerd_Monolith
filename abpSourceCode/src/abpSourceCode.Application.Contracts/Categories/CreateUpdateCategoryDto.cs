using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace abpSourceCode.Categories
{
    public class CreateUpdateCategoryDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Slug { get; set; }

        public string ImageUrl { get; set; }

        public string SeoTitle { get; set; }

        public string SeoDescription { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public List<Guid> Books { get; set; } = new List<Guid>();

    }
}
