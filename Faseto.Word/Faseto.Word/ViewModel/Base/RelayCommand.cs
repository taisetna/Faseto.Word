using System;
using System.Windows.Input;

namespace Faseto.Word
{
    class RelayCommand : ICommand
    {
        private Action m_Action { set; get; }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action _action)
        {
            m_Action = _action;
        }

        public bool CanExecute(object _parameter)
        {
            return true;
        }

        public void Execute(object _parameter)
        {
            m_Action();
        }
    }
}
