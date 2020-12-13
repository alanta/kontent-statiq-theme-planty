using Kentico.Kontent.Delivery.Abstractions;
using System.Linq;

namespace Planty.Models
{
    public partial class Product
    {
        public string Url => $"/products/{Slug}.html";
        public IAsset? Thumbnail => Image.FirstOrDefault();
    }
}