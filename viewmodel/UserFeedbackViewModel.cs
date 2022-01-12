using System;
using System.Collections.Generic;
using System.Text;
using WpfTestHarness.model;

namespace WpfTestHarness.viewmodel
{
    public class UserFeedbackViewModel : ViewModelBase
    {
        public UserFeedbackViewModel() : base()
        {
            DisplayStatusMessage("Your feedback are important!");
        }

        private UserFeedback _Entity = new UserFeedback();
        public UserFeedback Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
            }
        }
    }
}
