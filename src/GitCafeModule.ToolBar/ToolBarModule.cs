using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace GitCafeModule.ToolBar
{
    public class ToolBarModule : IModule
    {
        private IRegionViewRegistry regionViewRegistry;
        public ToolBarModule(IRegionViewRegistry region)
        {
            this.regionViewRegistry = region;
        }

        public void Initialize()
        {
            regionViewRegistry.RegisterViewWithRegion("ToolBarRegion", typeof(Views.ToolBarView));
        }
    }
}
