using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Planty.TagHelpers
{
    public class PlantyPicture : TagHelper
    {
        public string Alt { get; set; }
        public string Src { get; set; }
        public string CssClass { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "picture";
            var image = new TagBuilder("img");
            image.Attributes["alt"] = Alt;
            image.Attributes["src"] = Src;
            image.AddCssClass(CssClass);
            output.Content.AppendHtml(image);
        }
    }
}