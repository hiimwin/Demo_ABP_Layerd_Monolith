using abpSourceCode.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace abpSourceCode.Authors
{
    // base 
    public class AuthorAppService :
    CrudAppService<
        Author,
        AuthorDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateAuthorDto>,
    IAuthorAppService
    {
        public AuthorAppService(IRepository<Author, Guid> repository)
            : base(repository)
        {
            GetPolicyName = abpSourceCodePermissions.Authors.Default;
            GetListPolicyName = abpSourceCodePermissions.Authors.Default;
            CreatePolicyName = abpSourceCodePermissions.Authors.Create;
            UpdatePolicyName = abpSourceCodePermissions.Authors.Edit;
            DeletePolicyName = abpSourceCodePermissions.Authors.Delete;
        }
    }
}
