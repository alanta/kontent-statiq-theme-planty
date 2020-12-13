namespace Planty.Models
{
    public interface IPage
    {
        public MenuItem? MenuItem { get; }
        bool LightNavigation { get; }
        bool LightLogo { get; }
    }

}