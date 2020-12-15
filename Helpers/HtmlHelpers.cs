using Kentico.Kontent.Delivery.Abstractions;
using Kontent.Statiq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planty.Models;
using Planty.Pipelines;
using Statiq.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Site = Planty.Models.Site;

namespace Planty.Helpers
{
    public static class HtmlHelpers
    {
        public static string GetLink(this IHtmlHelper html, string relativeUri, bool includeHost = false)
        {
            var context = html.ViewData["StatiqExecutionContext"] as IExecutionContext;
            return context.GetLink(relativeUri, includeHost);
        }

        public static Product[] GetProducts(this IHtmlHelper html, ITaxonomyTerm category)
        {
            var context = html.ViewData["StatiqExecutionContext"] as IExecutionContext;
            return context.Outputs.FromPipeline(nameof(Products))
                .Where( d => (d["Category"] as ITaxonomyTerm)?.Codename == category.Codename )
                .Select(p => p.AsKontent<Product>()).ToArray();
        }

        public static Site Site(this IHtmlHelper html)
        {
            if( !html.ViewData.TryGetValue(PlantyKeys.Site, out var settings) ){
                throw new InvalidOperationException($"View data key {PlantyKeys.Site} was not set");
            }

            return settings as Site;
        }

        public static string LinkedPageOrUrl(this IHtmlHelper html, IEnumerable<object> linkedContent, string linkUrl)
        {
            var linkedPage = linkedContent.OfType<IPage>().FirstOrDefault();
            var url = linkedPage?.Url ?? linkUrl;

            return string.IsNullOrWhiteSpace(url) ? "#" : html.GetLink(url);
        }
    }
}