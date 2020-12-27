using Kentico.Kontent.Delivery.Abstractions;
using Kontent.Statiq;
using Planty.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Planty.Pipelines
{
    public class Categories : Pipeline
    {
        public Categories(SiteSettings settings)
        {
            Dependencies.AddRange(nameof(Products), nameof(Pages), nameof(Site));

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(Config.FromContext(ctx =>
                {
                    var store = ctx.Outputs.FromPipeline(nameof(Pages))
                        .First(doc => doc.AsKontent<Page>().System.Codename == "store");

                    // Pull in products from dependencies
                    var docs = ctx.Outputs.FromPipelines(nameof(Products));

                    // Group by category
                    var docsByCategory = docs.ToLookupMany( nameof(Product.Category), new TaxonomyTermComparer());

                    var categories = docsByCategory.Select(c => c.Key).ToArray();

                    return docsByCategory.Select(group =>
                    {
                        // Document per category
                        var metadata = new Dictionary<string, object>
                        {
                            {Keys.Title, group.Key.Name},
                            {Keys.Children, group.ToArray()},
                            {PlantyKeys.Categories, CategoryMenuItem.CategoryMenu(categories, group.Key)}
                        };

                        return store.Clone(store.Source, new NormalizedPath($"category/{group.Key.Codename}/index.html"), metadata);
                    });
                })),
                
                new MergeContent(new ReadFiles("layouts/category.cshtml")),
                new RenderRazor()
                    .WithViewData(PlantyKeys.Site, Config.FromDocument((doc, ctx) =>
                    {
                        var site = ctx.Outputs.FromPipeline(nameof(Site)).First().AsKontent<Models.Site>();
                        site.Settings = settings;
                        return site;
                    }))
                    .WithViewData(PlantyKeys.Title, Config.FromDocument( (doc,ctx) => doc[Keys.Title] ) )
                    .WithViewData(PlantyKeys.Categories, Config.FromDocument( (doc,ctx) => doc[PlantyKeys.Categories] ) )
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
