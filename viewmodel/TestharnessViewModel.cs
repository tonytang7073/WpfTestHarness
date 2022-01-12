using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTestHarness.viewmodel
{
    public class TestharnessViewModel : ViewModelBase
    {
        public TestharnessViewModel() : base()
        {
            DisplayStatusMessage("Welcome to the ICMS Test Harness!");
        }
    }
}
