using GitCafeCommon.Models;
using GitCafeCommon.PresentationEvent;
using GitCafeCommon.ViewModel;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GitCafeModule.RepositoryBox.ViewModels
{
    public class RepositoryBoxViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;
        private SubscriptionToken loadRepositoryDBSubscriptionToken;
        private SubscriptionToken addRepositoryDBSubscriptionToken;
        

        public RepositoryBoxViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Repositories = new BindingList<GitCafeRepository>();

            #region Load All Repository
            var loadRepositoryDBEvent = eventAggregator.GetEvent<LoadRepositoryDBEvent>();
            if (loadRepositoryDBSubscriptionToken != null)
            {
                loadRepositoryDBEvent.Unsubscribe(loadRepositoryDBSubscriptionToken);
            }
            loadRepositoryDBSubscriptionToken = loadRepositoryDBEvent.Subscribe(respositories =>
                {
                    foreach (var item in respositories)
                    {
                        Repositories.Add(item);
                    }
                }, ThreadOption.UIThread, false);
            #endregion

            #region Add Repository
            var addRepositoryEvent = eventAggregator.GetEvent<AddRepositoryDBEvent>();
            if (addRepositoryDBSubscriptionToken != null)
            {
                addRepositoryEvent.Unsubscribe(addRepositoryDBSubscriptionToken);
            }
            addRepositoryDBSubscriptionToken = addRepositoryEvent.Subscribe(rep =>
                {
                    Repositories.Add(rep);
                }, ThreadOption.UIThread, false);
            #endregion
        }

        public BindingList<GitCafeRepository> Repositories
        {
            get { return GetValue(() => Repositories); }
            set { SetValue(() => Repositories, value); }
        }

        public GitCafeRepository CurrentGitCafeRepositry
        {
            get { return GetValue(() => CurrentGitCafeRepositry); }
            set
            {
                SetValue(() => CurrentGitCafeRepositry, value);
                eventAggregator.GetEvent<ChangeRepositoryEvent>().Publish(value);
            }
        }

        private void AddGitCafeRepository(GitCafeRepository repository)
        {
            Repositories.Add(repository);
        }
    }
}
