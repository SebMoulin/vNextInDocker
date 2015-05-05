using System;

namespace WebSite3.Utils
{
    public static class HtmlHelper
    {
        public static object GetRemoveUserAccessButtonStyle(this Microsoft.AspNet.Mvc.Rendering.IHtmlHelper helper, bool hasDeveloper)
        {
            var style1 = new
            {
                onclick = "return confirm('Are you sure you want to remove the candidate this test environment? This will send the sonar stats to the report interface.');",
                @class = "btn btn-xs btn-danger ",
                style = "margin-right: 5px"
            };
            if (hasDeveloper) return style1;
            var style2 = new
            {
                @class = "btn btn-xs btn-danger ",
                style = "margin-right: 5px",
                disabled = "disabled"
            };
            return style2;
        }
    }
}