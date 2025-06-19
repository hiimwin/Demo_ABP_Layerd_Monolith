using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace abpSourceCode.Users
{
    public class UserInfor : AuditedAggregateRoot<Guid>
    {
        public string AvartaUrl { get; set; }

        public Guid UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
