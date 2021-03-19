using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ViewModel {
    public class CommandTemplate : ICommand {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion

        #region Constructors

        public CommandTemplate(Action<object> execute) : this(execute, null) { }

        public CommandTemplate(Action<object> execute, Predicate<object> canExecute) {
            if (execute == null)
                throw new ArgumentNullException();

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommandMembers

        [DebuggerStepThrough]
        public bool CanExecute(object parameter) {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }

        #endregion
    }
}
