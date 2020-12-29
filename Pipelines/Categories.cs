using Kontent.Statiq;
using Planty.Helpers;
using Planty.Models;
using Statiq.Common;
using Statiq.Core;
using System.Collections.Generic;
using System.Linq;

namespace Planty.Pipelines
{
    public class Categories : Pipeline
    {
        public Categories()
        {
            Dependencies.Add(nameof(Products));

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(Config.FromContext(ctx =>
                {
                    var products = ctx.Outputs.FromPipeline(nameof(Products));
                    var categories = products.SelectMany(p => p.AsKontent<Product>().Category)
                        .Distinct(new TaxonomyTermComparer())
                        .ToArray();

                    var metadata = new[] {new KeyValuePair<string, object>(PlantyKeys.Categories, categories)};
                    return ctx.CreateDocument(metadata).Yield();
                }))
            };
        }
       
    }
}
