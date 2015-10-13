using System;
using System.Windows.Input;

namespace Daisy.Terminal.ViewModels
{
    public abstract class WindowViewModelBase : ViewModelBase
    {
        private Command<WindowViewModelBase> _closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new Command<WindowViewModelBase>(param => RaiseRequestClose());
                }

                return _closeCommand;
            }
        }

        public event EventHandler RequestClose;


        protected void RaiseRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}