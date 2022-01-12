using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTestHarness.Exceptions;
using WpfTestHarness.MessageBroker;
using WpfTestHarness.model;

namespace WpfTestHarness.viewmodel
{
    public abstract class ViewModelBase : CommonBase
    {
        
        private string _displayName;
        public string DisplayName { get { return _displayName; }
            set {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }


        private ObservableCollection<ValidationMessage> _ValidationMessages = new ObservableCollection<ValidationMessage>();
        private bool _IsValidationVisible = false;
        public ObservableCollection<ValidationMessage> ValidationMessages
        {
            get { return _ValidationMessages; }
            set
            {
                _ValidationMessages = value;
                RaisePropertyChanged("ValidationMessages");
            }
        }

        public bool IsValidationVisible
        {
            get { return _IsValidationVisible; }
            set
            {
                _IsValidationVisible = value;
                RaisePropertyChanged("IsValidationVisible");
            }
        }


        public virtual void AddValidationMessage(string propertyName, string msg)
        {
            _ValidationMessages.Add(new ValidationMessage { Message = msg, PropertyName = propertyName });
            IsValidationVisible = true;
        }

        public virtual void Clear()
        {
            ValidationMessages.Clear();
            IsValidationVisible = false;
        }


        public virtual void Close(bool wasCancelled = true)
        {
            WpfTestHarness.MessageBroker.MessageBroker.Instance.SendMessage(MessageBrokerMessages.CLOSE_USER_CONTROL, wasCancelled);
        }


        public virtual void DisplayStatusMessage(string msg = "")
        {
            WpfTestHarness.MessageBroker.MessageBroker.Instance.SendMessage(MessageBrokerMessages.DISPLAY_STATUS_MESSAGE, msg);
        }


        public void PublishException(Exception ex)
        {
            // Publish Exception
            ExceptionManager.Instance.Publish(ex);
        }

    }
}
