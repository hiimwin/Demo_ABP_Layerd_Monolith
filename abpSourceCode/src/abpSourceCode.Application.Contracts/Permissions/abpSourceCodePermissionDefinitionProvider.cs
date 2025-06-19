using abpSourceCode.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace abpSourceCode.Permissions;

public class abpSourceCodePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(abpSourceCodePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(abpSourceCodePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<abpSourceCodeResource>(name);
    }
}
