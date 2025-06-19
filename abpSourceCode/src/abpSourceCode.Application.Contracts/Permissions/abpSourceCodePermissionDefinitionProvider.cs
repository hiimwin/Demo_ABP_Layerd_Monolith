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

        var booksPermission = myGroup.AddPermission(abpSourceCodePermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(abpSourceCodePermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(abpSourceCodePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(abpSourceCodePermissions.Books.Delete, L("Permission:Books.Delete"));

        var authors = myGroup.AddPermission(abpSourceCodePermissions.Authors.Default, L("Permission:Authors"));
        authors.AddChild(abpSourceCodePermissions.Authors.Create, L("Permission:Authors.Create"));
        authors.AddChild(abpSourceCodePermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authors.AddChild(abpSourceCodePermissions.Authors.Delete, L("Permission:Authors.Delete"));

        var payments = myGroup.AddPermission(abpSourceCodePermissions.Payments.Default, L("Permission:Payments"));
        payments.AddChild(abpSourceCodePermissions.Payments.Create, L("Permission:Payments.Create"));
        payments.AddChild(abpSourceCodePermissions.Payments.Edit, L("Permission:Payments.Edit"));
        payments.AddChild(abpSourceCodePermissions.Payments.Delete, L("Permission:Payments.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<abpSourceCodeResource>(name);
    }
}
