using GitCafeCommon.PresentationEvent;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GitCafeModule.Working.ViewModels
{
    public class WorkingViewModel:GitCafeCommon.ViewModel.ViewModelBase
    {
        private IEventAggregator eventAggregator;
        private SubscriptionToken recevieToolBarClickToken;

        public WorkingViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            NewOrClonePopupVisibility = Visibility.Hidden;

            var barClickEvent = eventAggregator.GetEvent<ToolBarClickEvent>();
            if (recevieToolBarClickToken != null)
            {
                barClickEvent.Unsubscribe(recevieToolBarClickToken);
            }
            recevieToolBarClickToken = barClickEvent.Subscribe(ShowNewOrCloneDialog, ThreadOption.UIThread, false);
        }

        public Visibility NewOrClonePopupVisibility
        {
            get { return GetValue(() => NewOrClonePopupVisibility); }
            set { SetValue(() => NewOrClonePopupVisibility, value); }
        }
        private void ShowNewOrCloneDialog(ToolBarClickType clickType)
        {
            if (clickType == ToolBarClickType.NewOrClone)
            {
                NewOrClonePopupVisibility = Visibility.Visible;
            }            
        }
    }
}
