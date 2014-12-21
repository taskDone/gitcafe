using GitCafeCommon.Models;
using GitCafeCommon.PresentationEvent;
using GitCafeCommon.ViewModel;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GitCafeModule.ToolBar.ViewModels
{
    public class ToolBarViewModel:ViewModelBase
    {
        private IEventAggregator eventAggregator;
        //private SubscriptionToken changeRepositorySubscriptionToken;

        public ToolBarViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            AddEnable = false;
            CommitEnable = false;

            NewOrCloneCommand = new DelegateCommand(CreateOrClone);
            AddCommand = new DelegateCommand(AddToRepository);
            CommitCommand = new DelegateCommand(CommitRepository);

            //var changeRepositoryEvent = eventAggregator.GetEvent<ChangeRepositoryEvent>();
            //if (changeRepositorySubscriptionToken != null)
            //{
            //    changeRepositoryEvent.Unsubscribe(changeRepositorySubscriptionToken);
            //}
            //changeRepositorySubscriptionToken = changeRepositoryEvent.Subscribe(RefreshButtonEnable,ThreadOption.UIThread,false);
        }

        #region property 
        /// <summary>
        /// c
        /// </summary>
        public ICommand NewOrCloneCommand { get; private set; }

        public ICommand CommitCommand { get; private set; }

        public ICommand AddCommand { get; private set; }

        public ICommand PushCommand { get; private set; }

        public bool CommitEnable
        {
            get { return GetValue(() => CommitEnable); }
            set { SetValue(() => CommitEnable, value); }
        }
        public bool AddEnable
        {
            get { return GetValue(() => AddEnable); }
            set { SetValue(() => AddEnable, value); }
        }
        #endregion

        private void CreateOrClone()
        {
            eventAggregator.GetEvent<ToolBarClickEvent>().Publish(ToolBarClickType.NewOrClone);
        }

        private void AddToRepository()
        {
            eventAggregator.GetEvent<ToolBarClickEvent>().Publish(ToolBarClickType.Add);
        }

        private void CommitRepository()
        {
            eventAggregator.GetEvent<ToolBarClickEvent>().Publish(ToolBarClickType.Commit);
        }

        private void RefreshButtonEnable(GitCafeRepository repository)
        {
            // working 
            var status =repository.Repository.RetrieveStatus();
            var work = status.Where(x => x.State == FileStatus.Untracked || x.State == FileStatus.Modified);
            if (work != null && work.Count() > 0)
            {
                AddEnable = true;
            }
            else
            {
                AddEnable = false;
            }
            var commit = status.Where(x => x.State == FileStatus.Added || x.State == FileStatus.Staged);
            if (commit != null && commit.Count() > 0)
            {
                CommitEnable = true;
            }
            else
            {
                CommitEnable = false;
            }
        }
        //public static Window GetCurrentWindow()
        //{
        //    return Enumerable.SingleOrDefault<Window>(Enumerable.Cast<Window>((IEnumerable)System.Windows.Application.Current.Windows), (Func<Window, bool>)(x => x.IsActive));
        //}
    }
}
