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
    public partial class MenuItem
    {
        public const string Codename = "menu_item";
        public const string TitleCodename = "title";
        public const string UrlCodename = "url";

        public IContentItemSystemAttributes System { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}