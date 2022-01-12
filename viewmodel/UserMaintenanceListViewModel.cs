using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfTestHarness.MessageBroker;
using WpfTestHarness.model;

namespace WpfTestHarness.viewmodel
{
    public class UserMaintenanceListViewModel : ViewModelAddEditDeleteBase
    {
        private ObservableCollection<AppUser> _users = new ObservableCollection<AppUser>();

        private MOCDAL _dbContext = null;


        public UserMaintenanceListViewModel(MOCDAL DbContext) : base()
        {
            _dbContext = DbContext;
        }


        public ObservableCollection<AppUser> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged("Users");
            }

        }

        public virtual void LoadUsers()
        {
            if (_dbContext == null) { return; }
            Users = new ObservableCollection<AppUser>();
        }

       
    }
}
