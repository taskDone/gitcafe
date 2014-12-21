using LibGit2Sharp;
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

namespace GitCafeModule.WorkSpace.Views
{
    /// <summary>
    /// WorkView.xaml 的交互逻辑
    /// </summary>
    public partial class WorkView : UserControl
    {
        private ViewModels.WorkSpaceViewModel vm;
        public WorkView(ViewModels.WorkSpaceViewModel vm)
        {
            InitializeComponent();

            this.vm = vm;
            this.DataContext = vm;
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var tvItem = e.OriginalSource as TreeViewItem;
            var header = tvItem.Header as Branch;
            if (header != null)
            {
                this.workingUC.Visibility = System.Windows.Visibility.Collapsed;
                this.commitUC.Visibility = System.Windows.Visibility.Visible;
                vm.Branch = header;
            }
        }

        private void workItem_Selected_1(object sender, RoutedEventArgs e)
        {
            this.workingUC.Visibility = System.Windows.Visibility.Visible;
            this.commitUC.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
