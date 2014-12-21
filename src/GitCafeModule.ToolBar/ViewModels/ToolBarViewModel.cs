using GitCafeCommon.PresentationEvent;
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
    public class ToolBarViewModel
    {
        private IEventAggregator eventAggregator;

        public ToolBarViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            NewOrCloneCommand = new DelegateCommand(CreateOrClone);

            AddCommand = new DelegateCommand(AddToRepository);

            CommitCommand = new DelegateCommand(CommitRepository);
        }

        #region property 
        public ICommand NewOrCloneCommand { get; private set; }
        public ICommand CommitCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand PushCommand { get; private set; }
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
        //public static Window GetCurrentWindow()
        //{
        //    return Enumerable.SingleOrDefault<Window>(Enumerable.Cast<Window>((IEnumerable)System.Windows.Application.Current.Windows), (Func<Window, bool>)(x => x.IsActive));
        //}
    }
}
