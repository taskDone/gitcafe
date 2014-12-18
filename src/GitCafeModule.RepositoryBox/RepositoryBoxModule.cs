using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace GitCafeModule.RepositoryBox
{
    public class RepositoryBoxModule : IModule
    {
        private IRegionViewRegistry regionViewRegistry;
        private IUnityContainer container;

        public RepositoryBoxModule(IUnityContainer container,IRegionViewRegistry region)
        {
            this.container = container;
            this.regionViewRegistry = region;
        }

        public void Initialize()
        {
            regionViewRegistry.RegisterViewWithRegion("RepositoryBoxRegion", typeof(Views.RepositoryBoxView));
        }
    }
}
