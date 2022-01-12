using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTestHarness.viewmodel;

namespace WpfTestHarness.model
{
    public class User : CommonBase
    {

        private int _UserId;
        private string _UserName = string.Empty;
        private string _Password = string.Empty;
        private string _FirstName = string.Empty;
        private string _LastName = string.Empty;
        private string _EmailAddress = string.Empty;
        private bool _IsLoggedIn = false;

        [Required]
        [Key]
        public int UserId
        {
            get { return _UserId; }
            set
            {
                _UserId = value;
                RaisePropertyChanged("UserId");
            }
        }

        [Required]
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                RaisePropertyChanged("UserName");
            }
        }

        [Required]
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                RaisePropertyChanged("Password");
            }
        }

        [Required]
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        [Required]
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        [Required]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set
            {
                _EmailAddress = value;
                RaisePropertyChanged("EmailAddress");
            }
        }

        [NotMapped]
        public bool IsLoggedIn
        {
            get { return _IsLoggedIn; }
            set
            {
                _IsLoggedIn = value;
                RaisePropertyChanged("IsLoggedIn");
            }
        }

    }
}
