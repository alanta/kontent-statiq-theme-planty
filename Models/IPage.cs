namespace Planty.Models
{
    public interface IPage
    {
        string Title { get; }
        string Url { get; }
        public MenuItem? MenuItem { get; }
        bool LightNavigation { get; }
        bool LightLogo { get; }
    }

}