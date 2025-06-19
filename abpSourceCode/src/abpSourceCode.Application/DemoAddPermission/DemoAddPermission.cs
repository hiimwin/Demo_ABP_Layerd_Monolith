using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace abpSourceCode.DemoAddPermission
{
    public class DemoAddPermission : ApplicationService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionManager _permissionManager;

        public DemoAddPermission(
            RoleManager<IdentityRole> roleManager,
            IPermissionManager permissionManager)
        {
            _roleManager = roleManager;
            _permissionManager = permissionManager;
        }
        public async Task GrantRolePermissionDemoAsync(
        string roleName, string permission)
        {
            await _permissionManager
                .SetForRoleAsync(roleName, permission, true);
        }

        public async Task GrantUserPermissionDemoAsync(
            Guid userId, string roleName, string permission)
        {
            await _permissionManager
                .SetForUserAsync(userId, permission, true);
        }

    }
}
