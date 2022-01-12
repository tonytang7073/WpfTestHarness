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
    /// Interaction logic for UserFeedbackControl.xaml
    /// </summary>
    public partial class UserFeedbackControl : UserControl
    {
        private UserFeedbackViewModel _viewModel = null;
        public UserFeedbackControl()
        {
            InitializeComponent();

            // Connect to instance of the view model created by the XAML
            _viewModel = (UserFeedbackViewModel)this.Resources["viewModel"];
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendFeedbackButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
