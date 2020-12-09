using Planty.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Handlebars;
using Statiq.Sass;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planty.Pipelines
{
    public class StyleSheets : Pipeline
    {
        public StyleSheets(SiteSettings settings)
        {
            InputModules = new ModuleList
            {
                new ReadFiles("sass/**/{!_,}*.scss"),
                // Apply palette from settings
                new RenderHandlebars()
                    .WithModel(Config.FromValue(settings)),
                CompileSass(settings.OptimizeOutput),
                new SetDestination(Config.FromDocument((doc, ctx) =>
                    new NormalizedPath($"assets/css/{doc.Source.FileNameWithoutExtension}.css"))),
                new WriteFiles()
            };
        }

        private CompileSass CompileSass(bool optimize)
        {
            var module = new CompileSass();
            return optimize ? module.WithCompressedOutputStyle() : module.WithCompactOutputStyle();
        }
    }
}
