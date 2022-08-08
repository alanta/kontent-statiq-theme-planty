using Kontent.Ai.Delivery.Abstractions;
using Kontent.Statiq;
using Planty.Helpers;
using Planty.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Linq;

namespace Planty.Pipelines
{
    public class ProductsByCategory : Pipeline
    {
        public ProductsByCategory(SiteSettings settings)
        {
            Dependencies.AddRange(nameof(Products), nameof(Pages), nameof(Site), nameof(Categories));

            ProcessModules = new ModuleList
            {
                // Pull in the products
                new ReplaceDocuments(nameof(Products)),
                // Group them by category (which is a Kontent Taxonomy term)
                new GroupDocuments(nameof(Product.Category)).WithComparer(new TaxonomyTermComparer()),
                // Set the title from the group
                new SetMetadata(Keys.Title, Config.FromDocument((doc,ctx)=>(doc[Keys.GroupKey] as ITaxonomyTerm).Name )),
                // Set the destination from the group
                new SetDestination(Config.FromDocument((doc,ctx) => new NormalizedPath($"category/{((ITaxonomyTerm)doc[Keys.GroupKey]).Codename}/index.html") )),
                // Pull the content in from the Store page
                new SetMetadata("KONTENT" /* TODO: replace with KontentKeys.Item */, Config.FromContext(ctx =>
                {
                    var store = ctx.Outputs.FromPipeline(nameof(Pages))
                        .First(doc => TypedContentExtensions.AsKontent<Page>(doc).System.Codename == "store");

                    return store.AsKontent<Page>();
                })),
                // Render the page
                new MergeContent(new ReadFiles("layouts/page.cshtml")),
                new RenderRazor()
                    .WithViewData(PlantyKeys.Site, Config.FromDocument((doc, ctx) =>
                    {
                        var site = ctx.Outputs.FromPipeline(nameof(Site)).First().AsKontent<Models.Site>();
                        site.Settings = settings;
                        return site;
                    }))
                    .WithViewData(PlantyKeys.Title, Config.FromDocument( (doc,ctx) => doc[Keys.Title] ) )
                    // Build the category menu
                    .WithViewData(PlantyKeys.Categories, Config.FromDocument( (doc,ctx) =>
                    {
                        var categories = ctx.Outputs.FromPipeline(nameof(Categories))
                            .First()[PlantyKeys.Categories] as ITaxonomyTerm[];

                        return categories.Select(category =>
                                new CategoryMenuItem(category,
                                    (doc[Keys.GroupKey] as ITaxonomyTerm).Codename == category.Codename))
                            .ToArray();
                    } ))
                    .WithViewData(PlantyKeys.Products, Config.FromDocument( (doc, ctx) => doc.GetChildren().Select( x => x.AsKontent<Product>()).ToArray() ) )
                    .WithModel( KontentConfig.As<Page>() ),
                new KontentImageProcessor(),
                //new OptimizeHtml()

            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}