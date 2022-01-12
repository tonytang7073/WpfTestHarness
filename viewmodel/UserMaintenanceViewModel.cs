using System;
using System.Collections.Generic;
using System.Text;
using WpfTestHarness.MessageBroker;
using WpfTestHarness.model;

namespace WpfTestHarness.viewmodel
{
    public class UserMaintenanceViewModel : UserMaintenanceDetailViewModel
    {



       
        public UserMaintenanceViewModel(MOCDAL dbContext) : base(dbContext)
        {
           
            DisplayStatusMessage("Manage Users");
        }

        public void DisplayPleaseWaitMessage(string msg="Loading data, please wait...")
        {
            WpfTestHarness.MessageBroker.MessageBroker.Instance.SendMessage(MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE, "Load Users");
            WpfTestHarness.MessageBroker.MessageBroker.Instance.SendMessage(MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE, msg);
        }

        public void ClearMessage()
        {
            WpfTestHarness.MessageBroker.MessageBroker.Instance.SendMessage(MessageBrokerMessages.CLEAR_MESSAGE);
        }
    }
}
