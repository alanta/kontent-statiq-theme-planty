using System;
using System.Collections.Generic;
using Kentico.Kontent.Delivery.Abstractions;
using System.Linq;

namespace Planty.Models
{
    public partial class FeaturedProductsSection
    {
        public bool LightTitle => Settings.Any(opt => opt.Codename == "light_title");
        public bool ShowHeartIcon => Settings.Any(opt => opt.Codename == "show_heart_icon");
    }
}