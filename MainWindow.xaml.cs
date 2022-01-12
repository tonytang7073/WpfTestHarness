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
using System.Windows.Threading;
using WpfTestHarness.viewmodel;
using WpfTestHarness.model;
using WpfTestHarness.MessageBroker;

namespace WpfTestHarness
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private variables
        // Main window's view model class
        private MainWindowViewModel _viewModel = null;
        // Hold the main window's original status message
        private string _originalMessage = string.Empty;


        #endregion
        public MainWindow()
        {
            InitializeComponent();
            // Connect to instance of the view model created by the XAML
            _viewModel = this.DataContext as MainWindowViewModel;

            // Get the original status message
            _originalMessage = _viewModel.StatusMessage;

            // Initialize the Message Broker Events
            WpfTestHarness.MessageBroker.MessageBroker.Instance.MessageReceived += Instance_MessageReceived;

        }

        public static IList<string> LoadSettingDelimitedString(string settingName)
        {
            return settingName.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        }

        private void Instance_MessageReceived(object sender, MessageBrokerEventArgs e)
        {
            switch (e.MessageName)
            {
                case MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE:
                    _viewModel.InfoMessageTitle = e.MessagePayload.ToString();
                    _viewModel.CreateInfoMessageTimer();
                    break;

                case MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE:
                    _viewModel.InfoMessage = e.MessagePayload.ToString();
                    _viewModel.CreateInfoMessageTimer();
                    break;

                case MessageBrokerMessages.DISPLAY_STATUS_MESSAGE:
                    // Set new status message
                    _viewModel.StatusMessage = e.MessagePayload.ToString();
                    break;

                case MessageBrokerMessages.LOGIN_SUCCESS:
                    _viewModel.UserEntity = (User)e.MessagePayload;
                    _viewModel.LoginMenuHeader = "Logout " + _viewModel.UserEntity.UserName;
                    break;

                case MessageBrokerMessages.LOGOUT:
                    _viewModel.UserEntity.IsLoggedIn = false;
                    _viewModel.LoginMenuHeader = "Login";
                    break;

                case MessageBrokerMessages.CLEAR_MESSAGE:
                    _viewModel.ClearInfoMessages();
                    break;

                case MessageBrokerMessages.CLOSE_USER_CONTROL:
                    CloseUserControl();
                    break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            string cmd = String.Empty;
            if (mi.Tag != null)
            {
                cmd = mi.Tag.ToString();
                if (cmd.Contains("."))
                {
                    LoadUserControl(cmd);
                }
                else
                {
                    // Process special commands
                    ProcessMenuCommands(cmd);
                }
            }
        }

        private void ProcessMenuCommands(string command)
        {
            switch (command.ToLower())
            {
                case "exit":
                    this.Close();
                    break;

                case "login":
                    if (_viewModel.UserEntity.IsLoggedIn)
                    {
                        // Logging out, so close any open windows
                        CloseUserControl();
                        // Reset the user object
                        _viewModel.UserEntity = new User();
                        // Make menu display Login
                        _viewModel.LoginMenuHeader = "Login";
                    }
                    else
                    {
                        // Display the login screen
                        LoadUserControl("WpfTestHarness.UserControls.LoginControl");
                    }
                    break;

                default:
                    break;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Call method to load resources application
            await LoadApplication();

            // Turn off informational message area
            _viewModel.ClearInfoMessages();
        }

        public async Task LoadApplication()
        {
            _viewModel.InfoMessage = "Loading State Codes...";
            await Dispatcher.BeginInvoke(new Action(() => {
                _viewModel.LoadStateCodes();
            }), DispatcherPriority.Background);

            _viewModel.InfoMessage = "Loading Country Codes...";
            await Dispatcher.BeginInvoke(new Action(() => {
                _viewModel.LoadCountryCodes();
            }), DispatcherPriority.Background);

            _viewModel.InfoMessage = "Loading Employee Types...";
            await Dispatcher.BeginInvoke(new Action(() => {
                _viewModel.LoadEmployeeTypes();
            }), DispatcherPriority.Background);
        }


        public void DisplayUserControl(UserControl uc)
        {
            contentArea.Children.Add(uc);
        }

        private void CloseUserControl()
        {
            contentArea.Children.Clear();
            _viewModel.StatusMessage = _originalMessage;
        }


        private void LoadUserControl(string controlName)
        {
            LoadUserControl(controlName, null);
        }
        private void LoadUserControl(string controlName, object param1)
        {
            Type ucType = null;
            UserControl uc = null;

            if (ShouldLoadUserControl(controlName))
            {
                // Create a Type from controlName parameter
                ucType = Type.GetType(controlName);
                if (ucType == null)
                {
                    MessageBox.Show("The Control: " + controlName
                                     + " does not exist.");
                }
                else
                {
                    // Close current user control in content area
                    // NOTE: Optionally add current user control to a list 
                    //       so you can restore it when you close the newly added one
                    CloseUserControl();

                    // Create an instance of this control
                    if (param1 == null)
                    {
                        uc = (UserControl)Activator.CreateInstance(ucType);
                    }
                    else
                    {
                        uc = (UserControl)Activator.CreateInstance(ucType, param1);
                    }


                    if (uc != null)
                    {
                        // Display control in content area
                        DisplayUserControl(uc);
                    }
                }
            }
        }

        private bool ShouldLoadUserControl(string controlName)
        {
            bool ret = true;

            // Make sure you don't reload a control already loaded.
            if (contentArea.Children.Count > 0)
            {
                if (((UserControl)contentArea.Children[0]).GetType().FullName == controlName)
                {
                    ret = false;
                }
            }

            return ret;
        }
    }
}

