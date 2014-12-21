using GitCafeCommon.Models;
using GitCafeCommon.PresentationEvent;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GitCafeModule.WorkSpace.ViewModels
{
    public class WorkSpaceViewModel : GitCafeCommon.ViewModel.ViewModelBase
    {
        private IEventAggregator eventAggregator;
        private SubscriptionToken changeRepositorySubscriptionToken;

        public WorkSpaceViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            var changeRepositoryEvent = eventAggregator.GetEvent<ChangeRepositoryEvent>();
            if (changeRepositorySubscriptionToken != null)
            {
                changeRepositoryEvent.Unsubscribe(changeRepositorySubscriptionToken);
            }
            changeRepositorySubscriptionToken = changeRepositoryEvent.Subscribe((r) =>
                {
                    GitCafeRepository = r;
                    OnPropertyChanged("Branches");
                    var branch = r.Repository.Branches["master"];
                    foreach (var commit in branch.Commits)
                    {
                        var id = commit.Id.ToString(7);
                        var message = commit.Message;
                        //MessageBox.Show(string.Format("ID:[{0}]{1}Message:[{2}]",id,Environment.NewLine, message));
                    }
                }, ThreadOption.UIThread, false);
        }

        public GitCafeRepository GitCafeRepository
        {
            get { return GetValue(() => GitCafeRepository); }
            set { SetValue(() => GitCafeRepository, value); }
        }

        public Branch Branch
        {
            get { return GetValue(() => Branch); }
            set { SetValue(() => Branch, value); }
        }

        public Commit Commit
        {
            get { return GetValue(() => Commit); }
            set
            {
                SetValue(() => Commit, value);
            }
        }
    }
}
