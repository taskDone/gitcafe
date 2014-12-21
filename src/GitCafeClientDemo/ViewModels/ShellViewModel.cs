using GitCafeCommon.Dao;
using GitCafeCommon.Models;
using GitCafeCommon.PresentationEvent;
using GitCafeCommon.ViewModel;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GitCafeClientDemo.ViewModels
{
    /// <summary>
    /// Shell View Model
    /// </summary>
    public class ShellViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;
        private IGitCafeRepositoryDao gitDao;
        private SubscriptionToken recevieToolBarClickToken;

        public ShellViewModel(IEventAggregator eventAggregator, IGitCafeRepositoryDao dao)
        {
            this.eventAggregator = eventAggregator;
            this.gitDao = dao;

            NewOrClonePopupVisibility = Visibility.Hidden;
            IsCloning = false;

            #region BarClickEvent
            var barClickEvent = eventAggregator.GetEvent<ToolBarClickEvent>();
            if (recevieToolBarClickToken != null)
            {
                barClickEvent.Unsubscribe(recevieToolBarClickToken);
            }
            recevieToolBarClickToken = barClickEvent.Subscribe(ToolBarHandler, ThreadOption.UIThread, false);
            #endregion

            NewOrCloneCacelCommand = new DelegateCommand(HideNewOrCloneDialog);

            CreateRepositoryCommand = new DelegateCommand(() =>
            {

            },()=>false);

            CloneCommand = new DelegateCommand(Clone);

            BrowserCommand = new DelegateCommand(() =>
                {
                    var dialog = new System.Windows.Forms.FolderBrowserDialog();
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    WorkPath = dialog.SelectedPath;
                });
        }

        /// <summary>
        /// 显示NewOrClone窗口
        /// </summary>
        public Visibility NewOrClonePopupVisibility
        {
            get { return GetValue(() => NewOrClonePopupVisibility); }
            set { SetValue(() => NewOrClonePopupVisibility, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand NewOrCloneCacelCommand { get; private set; }

        /// <summary>
        /// Clone
        /// </summary>
        public ICommand CloneCommand { get; private set; }

        /// <summary>
        /// 选择路径
        /// </summary>
        public ICommand BrowserCommand { get; private set; }

        public ICommand CreateRepositoryCommand { get; private set; }

        public string GitSource
        {
            get { return GetValue(() => GitSource); }
            set { SetValue(() => GitSource, value); }
        }
        public string WorkPath
        {
            get { return GetValue(() => WorkPath); }
            set { SetValue(() => WorkPath, value); }
        }

        public string CloneName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(GitSource);
            }
        }

        public bool IsCloning
        {
            get { return GetValue(() => IsCloning); }
            set { SetValue(() => IsCloning, value); }
        }

        #region Helper Method
        private void ToolBarHandler(ToolBarClickType clickType)
        {
            if (clickType == ToolBarClickType.NewOrClone)
            {
                ShowNewOrCloneDialog(true);
            }
        }

        private void HideNewOrCloneDialog()
        {
            ShowNewOrCloneDialog(false);
        }
        private void ShowNewOrCloneDialog(bool isShow)
        {
            if (isShow)
            {
                NewOrClonePopupVisibility = Visibility.Visible;
            }
            else
            {
                NewOrClonePopupVisibility = Visibility.Collapsed;
            }
        }

        private void Clone()
        {
            if (Directory.Exists(WorkPath))
            {
                IsCloning = true;
                string localGit = Repository.Clone(GitSource, WorkPath);
                GitCafeRepository rep = new GitCafeRepository { Name = CloneName, GitSource = localGit, WorkPath = WorkPath };
                gitDao.Add(rep);

                eventAggregator.GetEvent<AddRepositoryDBEvent>().Publish(rep);
                IsCloning = false;
                HideNewOrCloneDialog();
            }

        }
        private bool CanClone()
        {
            return Directory.Exists(WorkPath);
        }
        #endregion

    }
}
