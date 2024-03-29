﻿using Before_the_wedding.Models;
using Before_the_wedding.Services;
using Before_the_wedding.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public enum SexMode
        {
            Woman,
            Man
        }

        #region Interterface
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IDataPersonStore<Person> DataPersonStore => DependencyService.Get<IDataPersonStore<Person>>();
        public IDataStoreItemAnswer<ItemAnswer> DataStoreItemAnswer => DependencyService.Get<IDataStoreItemAnswer<ItemAnswer>>();
        public IDataLetter<Letter> DataStoreLetter => DependencyService.Get<IDataLetter<Letter>>();
        public IDataValue<Value> DataStoreValue => DependencyService.Get<IDataValue<Value>>();
        public IDataFeel<Feel> DataStoreFeel => DependencyService.Get<IDataFeel<Feel>>();
        #endregion
        public BaseViewModel()
        {
         

        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
