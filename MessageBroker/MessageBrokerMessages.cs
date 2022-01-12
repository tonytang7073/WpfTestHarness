﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestHarness.MessageBroker
{
    public class MessageBrokerMessages
    {
        public const string DISPLAY_STATUS_MESSAGE = "DisplayStatusMessage";

        public const string DISPLAY_TIMEOUT_INFO_MESSAGE = "DisplayTimeoutInfoMessage";
        public const string DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE = "DisplayTimeoutInfoMessageTitle";

        public const string CLOSE_USER_CONTROL = "CloseUserControl";
        public const string OPEN_USER_CONTROL = "OpenUserControl";

        public const string LOGIN_SUCCESS = "LoginSuccess";
        public const string LOGIN_FAIL = "LoginFail";
        public const string LOGOUT = "Logout";

        public const string CLEAR_MESSAGE = "ClearMessage";
    }
}
