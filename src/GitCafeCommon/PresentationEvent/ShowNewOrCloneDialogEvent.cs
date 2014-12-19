using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LibGit2Sharp;
namespace GitCafeCommon.PresentationEvent
{
    public class ToolBarClickEvent : CompositePresentationEvent<ToolBarClickType>
    {

    }

    public enum ToolBarClickType
    {
        NewOrClone,
        Commit,
        Add,
        Push
    }

    public class CurrentRepositoryEvent : CompositePresentationEvent<GitCafeRepository>
    {

    }
    public class GitCafeRepository:ViewModel.ViewModelBase
    {
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value); }
        }
        public string LocalPath
        {
            get { return GetValue(() => LocalPath); }
            set { SetValue(() => LocalPath, value); }
        }
        public Repository Repository
        {
            get { return GetValue(() => Repository); }
            set { SetValue(() => Repository, value); }
        }
    }
}
