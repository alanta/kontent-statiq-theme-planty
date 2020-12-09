using System;
using System.Collections.Generic;
using Kentico.Kontent.Delivery.Abstractions;
using System.Linq;

namespace Planty.Models
{
    public partial class CallToAction
    {
        public bool ShowArrow => Settings.Any(opt => opt.Codename == "arrow");
        public bool NewTab => Settings.Any(opt => opt.Codename == "new_tab");
    }
}