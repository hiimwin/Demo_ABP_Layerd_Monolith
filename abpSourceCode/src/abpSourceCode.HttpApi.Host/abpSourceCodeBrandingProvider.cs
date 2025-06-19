using Microsoft.Extensions.Localization;
using abpSourceCode.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace abpSourceCode;

[Dependency(ReplaceServices = true)]
public class abpSourceCodeBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<abpSourceCodeResource> _localizer;

    public abpSourceCodeBrandingProvider(IStringLocalizer<abpSourceCodeResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
