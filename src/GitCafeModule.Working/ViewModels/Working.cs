using GitCafeCommon.PresentationEvent;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

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

            NewOrCloneCacelCommand = new DelegateCommand(HideNewOrCloneDialog);

            var barClickEvent = eventAggregator.GetEvent<ToolBarClickEvent>();
            if (recevieToolBarClickToken != null)
            {
                barClickEvent.Unsubscribe(recevieToolBarClickToken);
            }
            recevieToolBarClickToken = barClickEvent.Subscribe(ToolBarHandler, ThreadOption.UIThread, false);
        }

        public Visibility NewOrClonePopupVisibility
        {
            get { return GetValue(() => NewOrClonePopupVisibility); }
            set { SetValue(() => NewOrClonePopupVisibility, value); }
        }

        public ICommand NewOrCloneCacelCommand { get;private set; }

        private void ToolBarHandler(ToolBarClickType clickType)
        {
            if (clickType == ToolBarClickType.NewOrClone)
            {
                ShowNewOrCloneDialog(true);
            }            
        }
        #region NewOrCloneDialog
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
        #endregion
    }
}
