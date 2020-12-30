using Microsoft.AspNetCore.Razor.TagHelpers;
using Planty.Models;
using Statiq.Common;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Planty.TagHelpers
{
    [HtmlTargetElement("snipcart-buy")]
    public class SnipcartBuyButton : TagHelper
    {
        public Product Product { get; set;}

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;

            // https://docs.snipcart.com/v3/setup/products
            
            // Base
            
            output.Attributes.SetAttribute("data-item-id", Product.ProductId);
            output.Attributes.SetAttribute("data-item-name", Product.Title);
            output.Attributes.SetAttribute("data-item-url", Product.Url);
            output.Attributes.SetAttribute("data-item-price", string.Format(CultureInfo.InvariantCulture, "{0:N2}", Product.Price));
            output.Attributes.SetAttribute("data-item-image", IExecutionContext.Current.GetLink( Product.Thumbnail?.Url, true )); // todo : size + ensure download
            output.Attributes.SetAttribute("data-item-description", Product.Description);

            // Quantity
            
            if (Product.MaximumQuantity > 0)
            {
                output.Attributes.SetAttribute("data-item-max-quantity", $"{Product.MaximumQuantity.Value:N0}");
            }
            if (Product.MinimumQuantity > 0)
            {
                output.Attributes.SetAttribute("data-item-min-quantity", $"{Product.MinimumQuantity.Value:N0}");
            }
            if (Product.DefaultQuantity > 0)
            {
                output.Attributes.SetAttribute("data-item-quantity", $"{Product.DefaultQuantity.Value:N0}");
            }
            if (Product.QuantityStep > 0)
            {
                output.Attributes.SetAttribute("data-item-quantity-step", $"{Product.QuantityStep.Value:N0}");
            }
            if (Product.Category.Any())
            {
                output.Attributes.SetAttribute("data-item-categories", string.Join("|", Product.Category.Select(c => c.Name)));
            }
            
            // Shipping
            
            if (Product.Width > 0)
            {
                output.Attributes.SetAttribute("data-item-width", $"{Product.Width.Value:N0}");
            }
            if (Product.Height > 0)
            {
                output.Attributes.SetAttribute("data-item-height", $"{Product.Height.Value:N0}");
            }
            if (Product.Length > 0)
            {
                output.Attributes.SetAttribute("data-item-length", $"{Product.Length.Value:N0}");
            }
            if (Product.Weight > 0)
            {
                output.Attributes.SetAttribute("data-item-weight", $"{Product.Weight.Value:N0}");
            }
            
            // Custom

            int propIndex = 1;
            foreach (var property in Product.CartOptions.OfType<ProductProperty>())
            {
                var prop = CreateProductPropertyAttributes(property, propIndex++);
                output.Attributes.AddRange(prop);
            }
            
            var childContent = output.Content.IsModified ? output.Content.GetContent() :
                (await output.GetChildContentAsync()).GetContent();

            output.Content.SetHtmlContent(childContent);
        }

        private TagHelperAttributeList CreateProductPropertyAttributes(ProductProperty property, int index)
        {
            var result = new TagHelperAttributeList();
            result.SetAttribute($"data-item-custom{index}-name", property.Name);
            if (property.Settings.Any(s => s.Codename == "required"))
            {
                result.SetAttribute($"data-item-custom{index}-required", "true");
            }
            
            if (!string.IsNullOrWhiteSpace(property.Value))
            {
                result.SetAttribute($"data-item-custom{index}-value", property.Value);
            }

            switch (property.Type.FirstOrDefault()?.Codename)
            {
                case "options":
                    result.SetAttribute($"data-item-custom{index}-options", property.Options);
                    break;
                case "textarea":
                    result.SetAttribute($"data-item-custom{index}-type", "textarea");
                    break;
                case "readonly":
                    result.SetAttribute($"data-item-custom{index}-type", "readonly");
                    break;
                case "text":
                default :
                    break;
                
            }

            return result;
        }
    }
}
