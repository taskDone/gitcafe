using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Events;
using GitCafeCommon.PresentationEvent;
using GitCafeCommon.Dao;

namespace GitCafeClientDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Shell : MetroWindow
    {
        private IEventAggregator eventAggregator;
        private IGitCafeRepositoryDao dao;

        public Shell(IEventAggregator eventAggregator,IGitCafeRepositoryDao dao,ViewModels.ShellViewModel vm)
        {
            InitializeComponent();

            this.eventAggregator = eventAggregator;
            this.dao = dao;
            this.DataContext = vm;

            this.Loaded += Shell_Loaded;
            this.Activated += Shell_Activated;
        }

        private void Shell_Activated(object sender, EventArgs e)
        {
            eventAggregator.GetEvent<WindowActiveEvent>().Publish(new WindowActiveEx());
        }

        void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            Action action = new Action(LoadRepository);
            action.BeginInvoke(null, null);
        }

        /// <summary>
        /// 加载DB中仓库信息
        /// </summary>
        void LoadRepository()
        {
            var repoDB = dao.Load();
            System.Threading.Thread.Sleep(200);
            eventAggregator.GetEvent<LoadRepositoryDBEvent>().Publish(repoDB);
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    var dialog = new System.Windows.Forms.FolderBrowserDialog();
        //    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
        //    txtWorkDir.Text = dialog.SelectedPath;
        //}
    }
}
