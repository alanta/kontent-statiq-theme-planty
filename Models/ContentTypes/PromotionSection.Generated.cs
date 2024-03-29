// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System;
using System.Collections.Generic;
using Kontent.Ai.Delivery.Abstractions;

namespace Planty.Models
{
    public partial class PromotionSection
    {
        public const string Codename = "promotion_section";
        public const string CtaLinkCodename = "cta_link";
        public const string CtaLinkedPageCodename = "cta_linked_page";
        public const string CtaTextCodename = "cta_text";
        public const string ImageCodename = "image";
        public const string SubtitleCodename = "subtitle";
        public const string TitleCodename = "title";

        public string CtaLink { get; set; }
        public IEnumerable<object> CtaLinkedPage { get; set; }
        public string CtaText { get; set; }
        public IEnumerable<IAsset> Image { get; set; }
        public string Subtitle { get; set; }
        public IContentItemSystemAttributes System { get; set; }
        public string Title { get; set; }
    }
}