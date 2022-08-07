using Kontent.Ai.Delivery.Abstractions;

namespace Planty.Models
{
    public interface IContentPage : IPage
    {

        IRichTextContent Content { get; }
    }
}