using System.Linq;

namespace Planty.Models
{
    public partial class Page : IContentPage
    {
        public string Url => Slug;
        public MenuItem? MenuItem => Section.OfType<MenuItem>().FirstOrDefault();

        public bool LightNavigation => Settings.Any(s => s.Codename == "white_header");
        public bool LightLogo => LightNavigation;
    }
}