using System;
using System.ComponentModel;
using System.IO;

namespace Daisy.Terminal.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public virtual string DisplayName { get; protected set; }


        public void Dispose()
        {
            OnDispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new InvalidDataException(string.Format("Invalid property name: {0}", propertyName));
            }
        }


        protected virtual void OnDispose()
        {
        }


        protected virtual void RaisePropertyChangedEvent(string propertyName)
        {
            VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}