using Kentico.Kontent.Delivery.Abstractions;

namespace Planty.Models
{
    public interface IContentPage : IPage
    {
        string Title {get;}
        string Url {get;}
        IRichTextContent Content { get; }
    }
}