using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using System.Text.Encodings.Web;

namespace Planty.TagHelpers
{
    [HtmlTargetElement("planty-picture")]
    public class PlantyPicture : TagHelper
    {
        public string Alt { get; set; }
        public string Src { get; set; }
        public string CssClass { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "picture";
            output.TagMode = TagMode.StartTagAndEndTag;
            var image = new TagBuilder("img");
            image.TagRenderMode = TagRenderMode.SelfClosing;
            image.Attributes["alt"] = Alt;
            image.Attributes["src"] = Src;
            image.AddCssClass(CssClass);

            var writer = new StringWriter();
            image.WriteTo(writer, HtmlEncoder.Default);
            
            output.Content.SetHtmlContent(image);
        }
    }
}