using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;

using GitCafeModule.ToolBar;
using GitCafeModule.RepositoryBox;
using Microsoft.Practices.Prism.Modularity;

namespace GitCafeClientDemo
{
    public class Bootstrapper:UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return this.Container.TryResolve<Shell>();
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            
            moduleCatalog.AddModule(typeof(ToolBarModule));
            moduleCatalog.AddModule(typeof(RepositoryBoxModule));
        }
    }
}
