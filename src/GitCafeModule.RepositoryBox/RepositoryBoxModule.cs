using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace GitCafeModule.RepositoryBox
{
    public class RepositoryBoxModule : IModule
    {
        private IRegionViewRegistry regionViewRegistry;
        public RepositoryBoxModule(IRegionViewRegistry region)
        {
            this.regionViewRegistry = region;
        }

        public void Initialize()
        {
            regionViewRegistry.RegisterViewWithRegion("RepositoryBoxRegion", typeof(Views.RepositoryBoxView));
        }
    }
}
