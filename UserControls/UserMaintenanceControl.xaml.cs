using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfTestHarness.viewmodel;

namespace WpfTestHarness.UserControls
{
    /// <summary>
    /// Interaction logic for UserMaintenanceControl.xaml
    /// </summary>
    public partial class UserMaintenanceControl : UserControl
    {

        private UserMaintenanceViewModel _viewModel = null;

        public UserMaintenanceControl()
        {
            InitializeComponent();
            // Connect to instance of the view model created by the XAML

            _viewModel = this.DataContext as UserMaintenanceViewModel;


        }



        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //_viewModel.LoadUsers(_dbContext); // this is a time consuming task. make it a background thread.
            await LoadUsers();

            //when done, clear the message.
            _viewModel.ClearMessage();
        }

        private async Task LoadUsers()
        {
            _viewModel.DisplayPleaseWaitMessage();
            await Dispatcher.BeginInvoke(new Action(() => { _viewModel.LoadUsers(); }), DispatcherPriority.Background);
        }

    }
}
