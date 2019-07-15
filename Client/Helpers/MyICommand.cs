using System;
using System.Windows.Input;

namespace Client.Helpers
{
    public class MyICommand : ICommand
    {
        private readonly Func<bool> _TargetCanExecuteMethod;
        private readonly Action _TargetExecuteMethod;

        public MyICommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public MyICommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }

            return _TargetExecuteMethod != null;
        }

        void ICommand.Execute(object parameter)
        {
            _TargetExecuteMethod?.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    public class MyICommand<T> : ICommand
    {
        private readonly Func<T, bool> _TargetCanExecuteMethod;
        private readonly Action<T> _TargetExecuteMethod;

        public MyICommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public MyICommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged = delegate { };

        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                var tparm = (T)parameter;
                return _TargetCanExecuteMethod(tparm);
            }

            return _TargetExecuteMethod != null;
        }

        void ICommand.Execute(object parameter)
        {
            _TargetExecuteMethod?.Invoke((T)parameter);
        }

        #endregion ICommand Members
    }
}