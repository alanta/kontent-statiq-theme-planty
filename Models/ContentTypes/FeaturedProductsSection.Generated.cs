// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System;
using System.Collections.Generic;
using Kentico.Kontent.Delivery.Abstractions;

namespace Planty.Models
{
    public partial class FeaturedProductsSection
    {
        public const string Codename = "featured_products_section";
        public const string FeaturedProductsCodename = "featured_products";
        public const string SettingsCodename = "settings";
        public const string TitleCodename = "title";

        public IEnumerable<object> FeaturedProducts { get; set; }
        public IEnumerable<IMultipleChoiceOption> Settings { get; set; }
        public IContentItemSystemAttributes System { get; set; }
        public string Title { get; set; }
    }
}