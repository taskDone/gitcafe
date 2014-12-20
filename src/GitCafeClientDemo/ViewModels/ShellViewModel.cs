using GitCafeCommon.PresentationEvent;
using GitCafeCommon.ViewModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GitCafeClientDemo.ViewModels
{
    public class ShellViewModel:ViewModelBase
    {
        private IEventAggregator eventAggregator;
        private SubscriptionToken recevieToolBarClickToken;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            NewOrClonePopupVisibility = Visibility.Hidden;

            var barClickEvent = eventAggregator.GetEvent<ToolBarClickEvent>();
            if (recevieToolBarClickToken != null)
            {
                barClickEvent.Unsubscribe(recevieToolBarClickToken);
            }
            recevieToolBarClickToken = barClickEvent.Subscribe(ToolBarHandler, ThreadOption.UIThread, false);

            NewOrCloneCacelCommand = new DelegateCommand(HideNewOrCloneDialog);
        }

        public Visibility NewOrClonePopupVisibility
        {
            get { return GetValue(() => NewOrClonePopupVisibility); }
            set { SetValue(() => NewOrClonePopupVisibility, value); }
        }

        public ICommand NewOrCloneCacelCommand { get; private set; }

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
    }
}
