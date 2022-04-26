using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Urls.Delivery.QueryParameters;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;

namespace Planty.Pipelines
{
    public class Site : Pipeline
    {
        public Site(IDeliveryClient deliveryClient)
        {
            InputModules = new ModuleList
            {
                new Kontent<Models.Site>(deliveryClient)
                    .WithQuery(new DepthParameter(2))
            };
        }
    }
}