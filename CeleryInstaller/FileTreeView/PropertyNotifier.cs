﻿using System;
using System.ComponentModel;

namespace CeleryInstaller.FileTreeView
{
    [Serializable]
    public abstract class PropertyNotifier : INotifyPropertyChanged
    {
        public PropertyNotifier() : base() { }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
