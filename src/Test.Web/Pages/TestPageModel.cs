using Test.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Test.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class TestPageModel : AbpPageModel
    {
        protected TestPageModel()
        {
            LocalizationResourceType = typeof(TestResource);
        }
    }
}