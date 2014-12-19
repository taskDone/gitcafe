using GitCafeCommon.PresentationEvent;
using GitCafeCommon.ViewModel;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GitCafeModule.RepositoryBox.ViewModels
{
    public class RepositoryBoxViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;

        public RepositoryBoxViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Repositories = new ObservableCollection<GitCafeRepository>();
            Action action = () =>
            {
                string defaultRepositoryPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.git\");
                Repository repository = new Repository(defaultRepositoryPath);
                var gitCafeRepo = new GitCafeRepository
                {
                    Name = "GitCafeClientDemo",
                    LocalPath = defaultRepositoryPath,
                    Repository = repository
                };
                Repositories.Add(gitCafeRepo);
            };
            action.BeginInvoke(null, null);
        }

        public ObservableCollection<GitCafeRepository> Repositories
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
                eventAggregator.GetEvent<CurrentRepositoryEvent>().Publish(value);
            }
        }

        private void AddGitCafeRepository(GitCafeRepository repository)
        {
            Repositories.Add(repository);
        }
    }
}
