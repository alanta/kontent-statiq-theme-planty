using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kontent.Statiq;
using Planty.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Linq;
using Page = Planty.Models.Page;

namespace Planty.Pipelines
{
    public class Products : Pipeline
    {
        public Products(IDeliveryClient deliveryClient, SiteSettings settings)
        {
            Dependencies.Add(nameof(Site));
            InputModules = new ModuleList
            {
                new Kontent<Product>(deliveryClient)
                    .WithQuery(new DepthParameter(2), new IncludeTotalCountParameter()),
                /*new SetMetadata(nameof(Page.Tags),
                    KontentConfig.Get<Page,ITaxonomyTerm[]>(post => post.Tags?.ToArray())),*/
                new SetMetadata(nameof(Product.Category),
                    KontentConfig.Get<Product,ITaxonomyTerm[]>(post => post.Category.ToArray())),
                new SetDestination(KontentConfig.Get((Product page) => new NormalizedPath( page.Url ))),
                /*new SetMetadata(SearchIndex.SearchItemKey, Config.FromDocument((doc, ctx) =>
                {
                    var page = doc.AsKontent<Page>();
                    return new LunrIndexDocItem(doc, page.Title, page.Body)
                    {
                        Description = page.MetadataMetaDescription,
                        //Tags = string.Join(", ", post.Tags.Select( t => t.Name ))
                    };
                })),*/
            };

            ProcessModules = new ModuleList
            {
                new MergeContent(new ReadFiles( "layouts/product.cshtml")),
                new RenderRazor()
                    .WithViewData(PlantyKeys.Site, Config.FromDocument((doc, ctx) =>
                    {
                        var site = ctx.Outputs.FromPipeline(nameof(Site)).First().AsKontent<Models.Site>();
                        site.Settings = settings;
                        return site;
                    }))
                    .WithViewData(PlantyKeys.Title, KontentConfig.Get<Product,string>( p => p.Title ))
                    .WithModel(KontentConfig.As<Product>()),
                new KontentImageProcessor(),
                //new OptimizeHtml()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles(),
            };
        }
    }
}