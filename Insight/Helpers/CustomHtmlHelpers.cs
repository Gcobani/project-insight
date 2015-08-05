using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Insight.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString PaperInput(this HtmlHelper helper, string label, string content)
        {
            string htmlString = String.Format("<paper-input label='{0}'>{1}</paper-input>", label, content);
            return new MvcHtmlString(htmlString);
        }
    }
}