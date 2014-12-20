using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

using GitCafeModule.ToolBar;
using GitCafeModule.RepositoryBox;
using GitCafeModule.WorkSpace;
using GitCafeCommon;

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

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<GitCafeCommon.Dao.SQLiteHelper>();
            Container.RegisterType<GitCafeCommon.Dao.IGitCafeRepositoryDao, GitCafeCommon.Dao.GieCafeRepositoryDao>();
            Container.RegisterType<GitCafeClientDemo.ViewModels.ShellViewModel>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            
            moduleCatalog.AddModule(typeof(ToolBarModule));
            moduleCatalog.AddModule(typeof(RepositoryBoxModule));
            moduleCatalog.AddModule(typeof(WorkingModule));
        }
    }
}
