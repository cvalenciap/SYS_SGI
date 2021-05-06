using System.Web.Mvc.Ajax;

namespace System.Web.Mvc
{
    public static class HelperMVC
    {
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            ajaxOptions.OnBegin = string.IsNullOrEmpty(ajaxOptions.OnBegin) ? "LoadLoading();" : string.Format("{0} {1}", "LoadLoading();", ajaxOptions.OnBegin);
            ajaxOptions.OnComplete = ajaxOptions.OnComplete == null ? "UnloadLoading();" : string.Concat(ajaxOptions.OnComplete, "UnloadLoading();");
            ajaxOptions.OnFailure = ajaxOptions.OnFailure == null ? "UnloadLoading();" : string.Concat(ajaxOptions.OnFailure, "UnloadLoading();");

            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }

        public static MvcHtmlString RawActionLinkToPopUp(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            ajaxOptions.OnBegin = string.IsNullOrEmpty(ajaxOptions.OnBegin) ? "LoadLoading();" : string.Format("{0} {1}", "LoadLoading();", ajaxOptions.OnBegin);
            ajaxOptions.OnComplete = ajaxOptions.OnComplete == null ? "UnloadLoading();" : string.Concat(ajaxOptions.OnComplete, "UnloadLoading();");
            ajaxOptions.OnFailure = ajaxOptions.OnFailure == null ? "UnloadLoading();" : string.Concat(ajaxOptions.OnFailure, "UnloadLoading();");

            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }

        private static AjaxOptions Configuration(AjaxOptions ajaxOptions)
        {
            ajaxOptions.OnBegin = string.IsNullOrEmpty(ajaxOptions.OnBegin) ? "LoadLoading();" : string.Concat(ajaxOptions.OnBegin, "LoadLoading();");
            ajaxOptions.OnComplete = string.IsNullOrEmpty(ajaxOptions.OnComplete) ? "UnloadLoading();" : string.Concat(ajaxOptions.OnComplete, "UnloadLoading();");
            ajaxOptions.OnFailure = string.IsNullOrEmpty(ajaxOptions.OnFailure) ? "UnloadLoading();" : string.Concat(ajaxOptions.OnFailure, "UnloadLoading();");

            return ajaxOptions;
        }
    }
}