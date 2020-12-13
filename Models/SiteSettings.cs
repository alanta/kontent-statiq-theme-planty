using System.Collections.Generic;

namespace Planty.Models
{
    public class SiteSettings{
        public string FavIcon {get; set;}
        public string LogoLight { get; set; }
        public string LogoDark { get; set; }
        public string SnipcartApiKey { get; set; }
        public string FooterText { get; set; }
        public bool OptimizeOutput { get; set; }
        public Dictionary<string, string> Palette { get; set; }
        public string HamburgerBackgroundImage { get; set; }
        public string BackgroundImagePrimary { get; set; }
        public string BackgroundImageSecondary { get; set; }
        public string BackgroundImageProduct { get; set; }
    }
}