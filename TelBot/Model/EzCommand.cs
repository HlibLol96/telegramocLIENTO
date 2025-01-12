using System.Windows;
using System.Windows.Input;


namespace TelBot.Model
{
    public class EzCommand
    {
        private bool internalDoer = true;
        private Action internalAction = null;

        private ICommand defCommand;
        public ICommand DefCommand => defCommand;

        public Action Action { get => internalAction; set => internalAction = value; }

        public EzCommand()
        {
            defCommand = new Command(DoCommand, Doer);
        }

        public void DoCommand(Object DoCommandObject)
        {
            if (internalAction == null)
            {
                MessageBox.Show("Nothing here");
            }
            else
            {
                internalAction();
            }
        }
        private bool Doer(Object DoerObject) => internalDoer;

        public Command Executor(Action<object> execute, Predicate<object> canExecute,Action action, bool? doer = true)
        {
            internalDoer = (bool)doer; 
            internalAction = action;
            
            return new Command(execute,canExecute);
        }
    }
}


/* private Command = ezcommand("DocommandObject","doerobject",true,action)
 * 
*/