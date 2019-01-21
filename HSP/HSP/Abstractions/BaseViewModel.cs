﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace HSP.Abstractions
{
    //implements the base functionality for each view.Aside from the 
    //INotifyPropertyChanged interface, there are some common 
    //properties we need for each page.Each page needs a title, 
    //for example, and each page needs an indicator of network 
    //activity.These can be placed in the Abstractions\BaseViewModel.cs class

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string _propTitle = string.Empty;
        bool _propIsBusy;

        public string title
        {
            get { return _propTitle; }
            set { SetProperty (ref _propTitle, value, "Title"); }
        }

        public bool IsBusy
        {
            get { return _propIsBusy; }
            set { SetProperty(ref _propIsBusy, value, "IsBusy"); }
        }

        protected void SetProperty<T>(ref T store, T value, string propName, Action onChanged = null)
        {
            if (IEqualityComparer<T>.Default.Equals(store, value))
                return;
            store = value;
            if (onChanged != null)
                onChanged();
            OnPropertyChanged(propName);

        }

        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
