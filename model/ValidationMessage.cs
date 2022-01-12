using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestHarness.model
{
    public class ValidationMessage : CommonBase
    {
        private string _PropertyName;
        private string _Message;

        public string PropertyName
        {
            get { return _PropertyName; }
            set
            {
                _PropertyName = value;
                RaisePropertyChanged("PropertyName");
            }
        }

        public string Message
        {
            get { return _Message; }
            set
            {
                _Message = value;
                RaisePropertyChanged("Message");
            }
        }


    }
}
