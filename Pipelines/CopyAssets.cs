using Statiq.Common;
using Statiq.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planty.Pipelines
{
    public class CopyAssets : Pipeline
    {
        public CopyAssets()
        {
            InputModules = new ModuleList
            {
                new ReadFiles(
                    "js/lib/*",
                    "js/*.js",
                    "js/*.vue",
                    "images/*"),
                new WriteFiles()
            };
        }

    }
}
