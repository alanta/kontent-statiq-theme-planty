using Kentico.Kontent.Delivery.Abstractions;

namespace Planty.Models
{
    public interface IContentPage : IPage
    {

        IRichTextContent Content { get; }
    }
}