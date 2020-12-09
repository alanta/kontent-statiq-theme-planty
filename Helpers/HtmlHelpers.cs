using Microsoft.AspNetCore.Mvc.Rendering;
using Planty.Pipelines;
using Statiq.Common;
using System;
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

        public static Site Site(this IHtmlHelper html)
        {
            if( !html.ViewData.TryGetValue(PlantyKeys.Site, out var settings) ){
                throw new InvalidOperationException($"View data key {PlantyKeys.Site} was not set");
            }

            return settings as Site;
        }
    }
}