using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Urls.Delivery.QueryParameters;
using Kontent.Statiq;
using Planty.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Linq;
using Page = Planty.Models.Page;

namespace Planty.Pipelines
{
    public class Pages : Pipeline
    {
        public Pages(IDeliveryClient deliveryClient, SiteSettings settings)
        {
            Dependencies.AddRange(nameof(Site), nameof(Categories));
            InputModules = new ModuleList
            {
                new Kontent<Page>(deliveryClient)
                    .WithQuery(new DepthParameter(2), new IncludeTotalCountParameter()),
                /*new SetMetadata(nameof(Page.Tags),
                    KontentConfig.Get<Page,ITaxonomyTerm[]>(post => post.Tags?.ToArray())),
                new SetMetadata(nameof(Page.Categories),
                    KontentConfig.Get<Page,ITaxonomyTerm[]>(post => post.Categories?.ToArray())),*/
                new SetDestination(KontentConfig.Get((Page page) => new NormalizedPath(  page.Url == "index" ? "index.html" : $"{page.Url}/index.html"))),
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
                new MergeContent(new ReadFiles( "layouts/page.cshtml")),
                new RenderRazor()
                    .WithViewData(PlantyKeys.Site, Config.FromDocument((doc, ctx) =>
                    {
                        var site = ctx.Outputs.FromPipeline(nameof(Site)).First().AsKontent<Models.Site>();
                        site.Settings = settings;
                        return site;
                    }))
                    .WithViewData(PlantyKeys.Categories, Config.FromDocument( (doc,ctx) =>
                    {
                        var categories = ctx.Outputs.FromPipeline(nameof(Categories))
                            .First()[PlantyKeys.Categories] as ITaxonomyTerm[];

                        return categories.Select(category =>
                                new CategoryMenuItem(category, true))
                            .ToArray();
                    } ))
                    .WithViewData(PlantyKeys.Products, Config.FromContext( ctx => 
                        ctx.Outputs.FromPipeline(nameof(Products)).Select( p => p.AsKontent<Product>() ).ToArray() ) 
                    )
                    .WithViewData(PlantyKeys.Title, KontentConfig.Get<Page,string>( p => p.Title ))
                    .WithModel(KontentConfig.As<Page>()),
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