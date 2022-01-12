using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTestHarness.viewmodel;

namespace WpfTestHarness.UserControls
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        private LoginViewModel _viewModel = null;
        public LoginControl()
        {
            InitializeComponent();
            // Connect to instance of the view model created by the XAML
            _viewModel = this.DataContext as LoginViewModel;
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Add the Password manually because data binding does not work
            _viewModel.Entity.Password = txtPassword.Password;

            _viewModel.Login();
        }
    }
}
