using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using System.Linq;

namespace Planty.Pipelines
{
    public class DownloadImages : Pipeline
    {
        public DownloadImages()
        {
            Dependencies.AddRange(nameof(Pages));
            PostProcessModules = new ModuleList(
                // pull documents from other pipelines
                new ReplaceDocuments(Dependencies.ToArray()),
                new KontentDownloadImages()
            );
            OutputModules = new ModuleList(

                new WriteFiles()
            );
        }
    }
}
