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
            {typeof(BulletPoint), "bullet_point"},
            {typeof(BulletPointsSection), "bullet_points_section"},
            {typeof(CallToAction), "call_to_action"},
            {typeof(ContactSection), "contact_section"},
            {typeof(FaqSection), "faq_section"},
            {typeof(FeaturedProductsSection), "featured_products_section"},
            {typeof(FrequentlyAskedQuestion), "frequently_asked_question"},
            {typeof(HeaderSection), "header_section"},
            {typeof(HeroSection), "hero_section"},
            {typeof(MenuItem), "menu_item"},
            {typeof(Page), "page"},
            {typeof(Product), "product"},
            {typeof(ProductProperty), "product_property"},
            {typeof(PromotionSection), "promotion_section"},
            {typeof(Site), "site"},
            {typeof(StoreSection), "store_section"},
            {typeof(Testimonial), "testimonial"},
            {typeof(TestimonialsSection), "testimonials_section"}
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