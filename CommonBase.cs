using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestHarness
{
    public abstract class CommonBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            // Grab a handler
            PropertyChangedEventHandler handler = this.PropertyChanged;
            // Only raise event if handler is connected
            if (handler != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

                // Raise the PropertyChanged event.
                handler(this, args);
            }
        }


        #region Clone Method
        public void Clone<T>(T original, T cloneTo)
        {
            if (original != null && cloneTo != null)
            {
                // Use reflection so the RaisePropertyChanged event is fired for each property
                foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var value = prop.GetValue(original, null);
                    prop.SetValue(cloneTo, value, null);
                }
            }
        }
        #endregion

    }
}
