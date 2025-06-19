using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace abpSourceCode.Categories
{
    public interface ICategoryAppService :
         ICrudAppService<
             CategoryDto,
             Guid,
             PagedAndSortedResultRequestDto,
             CreateUpdateCategoryDto>
    {

    }
}
