﻿using System;
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

namespace GitCafeModule.Working.Views
{
    /// <summary>
    /// WorkView.xaml 的交互逻辑
    /// </summary>
    public partial class WorkView : UserControl
    {
        public WorkView(ViewModels.WorkingViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
