using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace GitCafeModule.ToolBar
{
    public class ToolBarModule : IModule
    {
        private IRegionViewRegistry regionViewRegistry;
        private IUnityContainer container;
        public ToolBarModule(IUnityContainer container,IRegionViewRegistry region)
        {
            this.regionViewRegistry = region;
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<ViewModels.ToolBarViewModel>();
            container.RegisterType<Views.ToolBarView>();
            regionViewRegistry.RegisterViewWithRegion("ToolBarRegion", ()=>container.Resolve<Views.ToolBarView>());
        }
    }
}
