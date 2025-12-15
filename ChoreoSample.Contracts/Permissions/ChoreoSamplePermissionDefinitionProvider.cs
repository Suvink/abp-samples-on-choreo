using ChoreoSample.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ChoreoSample.Permissions;

public class ChoreoSamplePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ChoreoSamplePermissions.GroupName);


        var booksPermission = myGroup.AddPermission(ChoreoSamplePermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(ChoreoSamplePermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(ChoreoSamplePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(ChoreoSamplePermissions.Books.Delete, L("Permission:Books.Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(ChoreoSamplePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ChoreoSampleResource>(name);
    }
}
