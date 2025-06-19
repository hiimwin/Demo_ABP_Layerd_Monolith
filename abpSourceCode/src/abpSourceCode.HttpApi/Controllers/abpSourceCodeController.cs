using abpSourceCode.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace abpSourceCode.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class abpSourceCodeController : AbpControllerBase
{
    protected abpSourceCodeController()
    {
        LocalizationResource = typeof(abpSourceCodeResource);
    }
}
