using Kentico.Kontent.Delivery.Abstractions;
using System.Collections.Generic;

namespace Planty.Helpers
{
    public class TaxonomyTermComparer : IEqualityComparer<ITaxonomyTerm>
    {
        public bool Equals(ITaxonomyTerm? x, ITaxonomyTerm? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Codename == y.Codename;
        }

        public int GetHashCode(ITaxonomyTerm obj)
        {
            return (obj.Codename != null ? obj.Codename.GetHashCode() : 0);
        }
    }
}