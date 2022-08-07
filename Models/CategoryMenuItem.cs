using Kontent.Ai.Delivery.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Planty.Models
{
    public class CategoryMenuItem
    {
        public CategoryMenuItem(ITaxonomyTerm term, bool active)
        {
            Url = $"/category/{term.Codename}/";
            Title = term.Name;
            Active = active;
        }
        public string Url { get; }
        public string Title { get; }
        public bool Active { get; }

        public static CategoryMenuItem[] CategoryMenu(IEnumerable<ITaxonomyTerm> categories, ITaxonomyTerm current)
        {
            return categories.Select(c => new CategoryMenuItem(c, c.Codename == current.Codename)).ToArray();
        }
    }
}
