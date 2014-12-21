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
        private SubscriptionToken recevieToolBarClickToken;

        public WorkSpaceViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            #region ChangeRepository
            var changeRepositoryEvent = eventAggregator.GetEvent<ChangeRepositoryEvent>();
            if (changeRepositorySubscriptionToken != null)
            {
                changeRepositoryEvent.Unsubscribe(changeRepositorySubscriptionToken);
            }
            changeRepositorySubscriptionToken = changeRepositoryEvent.Subscribe((r) =>
                {
                    GitCafeRepository = r;
                }, ThreadOption.UIThread, false);
            #endregion

            var barClickEvent = eventAggregator.GetEvent<ToolBarClickEvent>();
            if (recevieToolBarClickToken != null)
            {
                barClickEvent.Unsubscribe(recevieToolBarClickToken);
            }
            recevieToolBarClickToken = barClickEvent.Subscribe(ToolBarHandler, ThreadOption.UIThread, false);
        }

        public GitCafeRepository GitCafeRepository
        {
            get { return GetValue(() => GitCafeRepository); }
            set
            {
                SetValue(() => GitCafeRepository, value);
                if (value != null)
                {
                    Branches = value.Repository.Branches.Where(x => !x.IsRemote);
                    RefreshWorking();
                }
            }
        }

        public IEnumerable<Branch> Branches
        {
            get { return GetValue(() => Branches); }
            set { SetValue(() => Branches, value); }
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

                if (value != null)
                {
                    var parentCommit = value.Parents;
                    if (parentCommit == null || parentCommit.Count() < 1)
                    {
                        TreeChanges changes = this.GitCafeRepository.Repository.Diff.Compare<TreeChanges>(value.Tree, DiffTargets.Index);
                        FileDetails = new List<TreeEntryChanges>();
                        foreach (TreeEntryChanges treeEntryChanges in changes)
                        {
                            FileDetails.Add(treeEntryChanges);
                        }
                    }
                    else
                    {
                        var tree = value.Tree;
                        var parentTree = value.Parents.Single().Tree;

                        TreeChanges changes = this.GitCafeRepository.Repository.Diff.Compare<TreeChanges>(parentTree, tree);
                        FileDetails = new List<TreeEntryChanges>();
                        foreach (TreeEntryChanges treeEntryChanges in changes)
                        {
                            FileDetails.Add(treeEntryChanges);
                        }
                    }
                }
            }
        }

        public List<TreeEntryChanges> FileDetails
        {
            get { return GetValue(() => FileDetails); }
            set { SetValue(() => FileDetails, value); }
        }

        public IEnumerable<StatusEntry> UnTrackedStatus
        {
            get { return GetValue(() => UnTrackedStatus); }
            set { SetValue(() => UnTrackedStatus, value); }
        }

        public IEnumerable<StatusEntry> AddedStatus
        {
            get { return GetValue(() => AddedStatus); }
            set { SetValue(() => AddedStatus, value); }
        }

        public IEnumerable<StatusEntry> Status
        {
            get { return GetValue(() => Status); }
            set { SetValue(() => Status, value); }
        }

        private void RefreshWorking()
        {
            if (this.GitCafeRepository != null)
            {
                Status = this.GitCafeRepository.Repository.RetrieveStatus();
                UnTrackedStatus = Status.Where(x => x.State == FileStatus.Untracked || x.State == FileStatus.Modified);                
                AddedStatus = Status.Where(x => x.State == FileStatus.Added || x.State == FileStatus.Staged);
            }
            
        }
        private void ToolBarHandler(ToolBarClickType clickType)
        {
            if (ToolBarClickType.Add == clickType)
            {
                AddToDB();
            }
            else if (ToolBarClickType.Commit == clickType)
            {
                CommitToDB();
            }
        }
        private void AddToDB()
        {
            foreach (var item in UnTrackedStatus)
            {
                this.GitCafeRepository.Repository.Stage(item.FilePath);
            }

            RefreshWorking();
        }

        private void CommitToDB()
        {
            try
            {
                this.GitCafeRepository.Repository.Commit("test commit");
            }
            catch { }
            RefreshWorking();
        }
    }
}
