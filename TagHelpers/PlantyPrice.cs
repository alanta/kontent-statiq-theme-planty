using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;

namespace Planty.TagHelpers
{
    [HtmlTargetElement("price")]
    public class PlantyPrice : TagHelper
    {
        public decimal? Value { get; set;}
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            if (Value.HasValue)
            {
                output.Content.SetHtmlContent(string.Format(CultureInfo.InvariantCulture, "&euro;{0:N2}", Value.Value)); // TODO : currency and locale
            }
            else
            {
                output.Content.SetHtmlContent("TBA");
            }
        }
    }
}