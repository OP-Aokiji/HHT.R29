using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Common.ResourceManager
{
    public class MessageResourceManager
    {
        protected System.Resources.ResourceManager rm;
        protected static Framework.Common.ResourceManager.MessageResourceManager instance;

        protected MessageResourceManager() 
        {
			initializeFacade();	
		}

        static MessageResourceManager()
        {
            instance = new MessageResourceManager();
        }

        protected virtual void initializeFacade()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            rm = new System.Resources.ResourceManager("Framework.Common.ResourceManager.MessageResource", assembly);
		}

        public static Framework.Common.ResourceManager.MessageResourceManager getInstance()
        {
            return instance;
        }

        public string getString(string title)
        {
            return rm.GetString(title);
        }
    }
}
