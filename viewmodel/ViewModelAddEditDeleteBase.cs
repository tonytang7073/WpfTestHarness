using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTestHarness.viewmodel
{
    public abstract class ViewModelAddEditDeleteBase : ViewModelBase
    {
        private bool _IsListEnabled = true;
        private bool _IsDetailEnabled = false;
        private bool _IsAddMode = false;

       

        public bool IsListEnabled
        {
            get { return _IsListEnabled; }
            set
            {
                _IsListEnabled = value;
                RaisePropertyChanged("IsListEnabled");
            }
        }

        public bool IsDetailEnabled
        {
            get { return _IsDetailEnabled; }
            set
            {
                _IsDetailEnabled = value;
                RaisePropertyChanged("IsDetailEnabled");
            }
        }

        public bool IsAddMode
        {
            get { return _IsAddMode; }
            set
            {
                _IsAddMode = value;
                RaisePropertyChanged("IsAddMode");
            }
        }

        /// <summary>
        /// Add/Edit a record state
        /// </summary>
        /// <param name="isAddMode"></param>
        public virtual void BeginEdit(bool isAddMode = false)
        {
            IsListEnabled = false;
            IsDetailEnabled = true;
            IsAddMode = isAddMode;
        }

        /// <summary>
        /// Reset the state back to dispaly a list of records
        /// </summary>
        public virtual void CancelEdit()
        {
            base.Clear();

            IsListEnabled = true;
            IsDetailEnabled = false;
            IsAddMode = false;
        }


        public virtual bool Save()
        {
            return true;
        }

        public virtual bool Delete()
        {
            return true;
        }



    }
}
