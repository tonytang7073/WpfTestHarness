using System;
using System.Collections.Generic;
using System.Text;
using WpfTestHarness.model;

namespace WpfTestHarness.viewmodel
{
    public class UserMaintenanceDetailViewModel : UserMaintenanceListViewModel
    {
        private AppUser _Entity = new AppUser();

        public UserMaintenanceDetailViewModel() 
        {

        }

        public UserMaintenanceDetailViewModel(MOCDAL dbContext) : base(dbContext)
        {

        }

        public AppUser Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
            }
        }


        public override void LoadUsers()
        {
            base.LoadUsers();

            if (Users.Count > 0)
            {
                Entity = Users[0];
            }
        }

        public override bool Save()
        {
            // TODO: Save User
            CancelEdit();
            return true;
        }

        public override bool Delete()
        {
            // TODO: Delete User
            return true;
        }



    }
}
