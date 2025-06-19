using abpSourceCode.Authors;
using abpSourceCode.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace abpSourceCode.Payments
{
    public class PaymentAppService :
    CrudAppService<
       Payment,
       PaymentDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdatePaymentDto>,
   IPaymentAppService
    {

        public PaymentAppService(IRepository<Payment, Guid> repository)
            : base(repository)
        {
            GetPolicyName = abpSourceCodePermissions.Payments.Default;
            GetListPolicyName = abpSourceCodePermissions.Payments.Default;
            CreatePolicyName = abpSourceCodePermissions.Payments.Create;
            UpdatePolicyName = abpSourceCodePermissions.Payments.Edit;
            DeletePolicyName = abpSourceCodePermissions.Payments.Delete;
        }

    }
}
