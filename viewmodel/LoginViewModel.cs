﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTestHarness.MessageBroker;
using WpfTestHarness.model;

namespace WpfTestHarness.viewmodel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel() : base()
        {
            DisplayStatusMessage("Login to Application");

            Entity = new User
            {
                UserName = Environment.UserName
            };
        }

        private User _Entity;

        public User Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
            }
        }
        public bool Validate()
        {
            bool ret = false;

            Entity.IsLoggedIn = false;
            ValidationMessages.Clear();
            if (string.IsNullOrEmpty(Entity.UserName))
            {
                AddValidationMessage("UserName", "User Name Must Be Filled In");
            }
            if (string.IsNullOrEmpty(Entity.Password))
            {
                AddValidationMessage("Password", "Password Must Be Filled In");
            }

            ret = (ValidationMessages.Count == 0);

            return ret;
        }

        public bool ValidateCredentials()
        {
            bool ret = false;
            //SampleDbContext db = null;

            try
            {
               // db = new SampleDbContext();

                // NOTE: Not using password here, but in production you would. I intentionally leave it out so as not having to go into hashing and securing your password.
                if (Entity.UserName == "tangz")
                {
                    ret = true;
                }
                else
                {
                    AddValidationMessage("LoginFailed",  "Invalid User Name and/or Password.");
                }
            }
            catch (Exception ex)
            {
                PublishException(ex);
            }

            return ret;
        }

        public bool Login()
        {
            bool ret = false;

            if (Validate())
            {
                // Check Credentials in User Table
                if (ValidateCredentials())
                {
                    // Mark as logged in
                    Entity.IsLoggedIn = true;

                    // Send message that login was successful
                    WpfTestHarness.MessageBroker.MessageBroker.Instance.SendMessage(MessageBrokerMessages.LOGIN_SUCCESS, Entity);

                    // Close the user control
                    Close(false);

                    ret = true;
                }
            }

            return ret;
        }

        public override void Close(bool wasCancelled = true)
        {
            if (wasCancelled)
            {
                // Display Informational Message
                WpfTestHarness.MessageBroker.MessageBroker.Instance.SendMessage(MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE, "User NOT Logged In.");
            }

            base.Close(wasCancelled);
        }
    }
}
