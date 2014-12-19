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

namespace GitCafeClientDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Shell : MetroWindow
    {
        private IEventAggregator eventAggregator;

        public Shell(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.eventAggregator = eventAggregator;

            this.Loaded += Shell_Loaded;
        }

        void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string defaultRepositoryPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.git\");
            //    Repository repository = new Repository(defaultRepositoryPath);
            //    eventAggregator.GetEvent<CurrentRepositoryEvent>().Publish(new GitCafeRepository
            //    {
            //        Name = "GitCafeClientDemo",
            //        LocalPath = defaultRepositoryPath,
            //        Repository = repository
            //    });
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
    }
}
