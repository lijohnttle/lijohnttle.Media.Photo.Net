using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace lijohnttle.Media.Photo.App.ViewModels.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T GetPropertyValue<T>(string propertyName)
        {
            if (_properties.TryGetValue(propertyName, out object value))
            {
                return (T)value;
            }
            else
            {
                return default(T);
            }
        }

        protected bool SetPropertyValue<T>(string propertyName, T value, Action actionBeforeNotify = null)
        {
            T oldValue = GetPropertyValue<T>(propertyName);

            if (ReferenceEquals(oldValue, value))
            {
                return false;
            }

            if (oldValue != null && oldValue.Equals(value))
            {
                return false;
            }

            _properties[propertyName] = value;

            if (actionBeforeNotify != null)
            {
                actionBeforeNotify();
            }

            NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}
