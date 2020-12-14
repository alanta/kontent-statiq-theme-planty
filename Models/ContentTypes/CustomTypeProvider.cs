using System;
using System.Collections.Generic;
using System.Linq;
using Kentico.Kontent.Delivery.Abstractions;

namespace Planty.Models
{
    public class CustomTypeProvider : ITypeProvider
    {
        private static readonly Dictionary<Type, string> _codenames = new Dictionary<Type, string>
        {
            {typeof(CallToAction), "call_to_action"},
            {typeof(FeaturedProductsSection), "featured_products_section"},
            {typeof(HeroSection), "hero_section"},
            {typeof(MenuItem), "menu_item"},
            {typeof(Page), "page"},
            {typeof(Product), "product"},
            {typeof(PromotionSection), "promotion_section"},
            {typeof(Site), "site"},
            {typeof(StoreSection), "store_section"}
        };

        public Type GetType(string contentType)
        {
            return _codenames.Keys.FirstOrDefault(type => GetCodename(type).Equals(contentType));
        }

        public string GetCodename(Type contentType)
        {
            return _codenames.TryGetValue(contentType, out var codename) ? codename : null;
        }
    }
}