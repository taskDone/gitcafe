using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace GitCafeModule.Working
{
    public class WorkingModule : IModule
    {
        private IRegionViewRegistry regionViewRegistry;
        private IUnityContainer container;

        public WorkingModule(IUnityContainer container,IRegionViewRegistry region)
        {
            this.container = container;
            this.regionViewRegistry = region;
        }
        public void Initialize()
        {
            container.RegisterType<ViewModels.WorkingViewModel>();
            container.RegisterType<Views.WorkView>();
            regionViewRegistry.RegisterViewWithRegion("WorkingRegion", () => container.Resolve<Views.WorkView>());
        }
    }
}
