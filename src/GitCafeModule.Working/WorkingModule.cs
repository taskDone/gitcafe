using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace GitCafeModule.Working
{
    public class WorkingModule : IModule
    {
        private IRegionViewRegistry regionViewRegistry;
        public WorkingModule(IRegionViewRegistry region)
        {
            this.regionViewRegistry = region;
        }
        public void Initialize()
        {
            regionViewRegistry.RegisterViewWithRegion("WorkingRegion", typeof(Views.WorkView));
        }
    }
}
