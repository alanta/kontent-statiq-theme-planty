using Kontent.Ai.Delivery.Abstractions;
using System.Linq;

namespace Planty.Models
{
    public partial class Product : IPage
    {
        public string Url => $"products/{Slug}.html";
        public IAsset? Thumbnail => Image.FirstOrDefault();
        public MenuItem? MenuItem => Section.OfType<MenuItem>().FirstOrDefault();
        public bool LightNavigation => false;
        public bool LightLogo => true;
    }
}