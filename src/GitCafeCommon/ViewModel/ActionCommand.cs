using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GitCafeCommon.ViewModel
{
    public class ActionCommand : ICommand
    {
        private readonly Action executeHandler;
        private readonly Action<object> executeHandlerWithParameter;
        private readonly Func<object, bool> canExecuteHandler;

        /// <summary>
        /// Parameterless version of the ActionCommand
        /// </summary>
        /// <param name="execute"></param>
        public ActionCommand(Action execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("Execute can't be null", new Exception());
            }

            this.executeHandler = execute;
        }

        /// <summary>
        /// ActionCommand with an object-parameter
        /// </summary>
        /// <param name="execute"></param>
        public ActionCommand(Action<object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("Execute can't be null", new Exception());
            }

            this.executeHandlerWithParameter = execute;
        }

        /// <summary>
        /// ActionCommand with an object-parameter and a CanExecute delegate
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"> </param>
        public ActionCommand(Action<object> execute, Func<object, bool> canExecute)
            : this(execute)
        {
            this.canExecuteHandler = canExecute;
        }

        #region Members of the 'ICommand' interface
        public void Execute(object args)
        {
            if (this.executeHandlerWithParameter != null)
            {
                this.executeHandlerWithParameter(args);
            }
            else
            {
                this.executeHandler();
            }
        }

        public bool CanExecute(object args)
        {
            if (this.canExecuteHandler == null)
            {
                return true;
            }

            return this.canExecuteHandler(args);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion
    }
}
