using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTestHarness.Exceptions
{
    public class ExceptionManager : CommonBase
    {
        private static ExceptionManager _Instance;
        public static ExceptionManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ExceptionManager();
                }

                return _Instance;
            }
            set { _Instance = value; }
        }

        public virtual void Publish(Exception ex)
        {
            // TODO: Implement an exception publisher here
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
    }
}
