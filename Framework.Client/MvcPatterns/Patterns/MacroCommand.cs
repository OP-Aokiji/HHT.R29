#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-05-28   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.Collections;
using MvcPatterns.Interfaces;

namespace MvcPatterns.Patterns
{
    public class MacroCommand : Notifier, ICommand
    {
        private IList subCommands;
		
        public MacroCommand()
		{
			subCommands = new ArrayList();
			initializeMacroCommand();			
		}
		
        protected virtual void initializeMacroCommand()
		{ }

        protected void addSubCommand( Type commandType )
		{
            subCommands.Add(commandType);
		}
		
		public void execute(INotification notification)
		{
			while (subCommands.Count > 0) 
            {
                Type commandType = (Type)subCommands[0];
                Object commandInstance = Activator.CreateInstance(commandType);
                if (commandInstance is ICommand)
                {
                    ((ICommand)commandInstance).execute(notification);
                }
                subCommands.RemoveAt(0);
			}
		}
    }
}
