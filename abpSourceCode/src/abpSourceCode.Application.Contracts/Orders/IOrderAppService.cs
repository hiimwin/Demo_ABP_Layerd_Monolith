using abpSourceCode.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace abpSourceCode.Orders
{
    public interface IOrderAppService :
     ICrudAppService<
         OrderDto,
         Guid,
         PagedAndSortedResultRequestDto,
         CreateUpdateOrderDto>
    {

    }
}
